using System;
using UnityEngine;
using Cinemachine;

public class Player : MonoBehaviour
{
    [Header("Cài đặt di chuyển")]
    [SerializeField] private float turnSpeed = 10f;
    private bool isMoveLeft = false;
    private bool isMoveRight = false;

    [Header("Cài đặt bánh xe")]
    [SerializeField] private Transform frontLeftWheel;
    [SerializeField] private Transform frontRightWheel;
    [SerializeField] private Transform rearLeftWheel;
    [SerializeField] private Transform rearRightWheel;
    [SerializeField] private GameObject trail;
    [SerializeField] private ParticleSystem[] smokes;

    [Header("Cài đặt mặt đất (Hệ thống giảm xóc)")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float rayLength = 1.5f;      // Tăng tia dài ra một chút để quét chạm đất
    [SerializeField] private float rideHeight = 0.5f;     // Chiều cao gầm xe cách mặt đất
    [SerializeField] private float suspensionForce = 15000f; // Lực đẩy lò xo nâng xe
    [SerializeField] private float suspensionDamping = 1500f; // Lực cản giảm xóc để xe không bị nảy tưng tưng
    public bool isCarGrounded = false;

    [Header("Cài đặt tốc độ và bẻ lái")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float speedturn = 4f;
    [SerializeField] private float speedminturn = 2f;
    [SerializeField] private float acceleration = 1f;

    [Header("Cài đặt Tai nạn")]
    public float popUpForce = 10000f;
    public float slideFriction = 10f;
    public float obstacleKnockback = 2000f;
    private bool isCrashed = false;

    [Header("Cài đặt Camera")]
    [SerializeField] private Rigidbody camera_rb;

    private float currentSpeed;
    private Rigidbody rb;
    private float maxSteerAngle = 30f;
    private float maxX = 20f;
    public float downforce = 50f;

    public static Player instance { get; private set; }

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private float turnDirection = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0f, -0.5f, 0f);
    }

    private void FixedUpdate()
    {
        GetInput();

        // 1. Áp dụng hệ thống giảm xóc đệm khí cho 4 bánh
        ApplyHoverSuspension();

        // 2. Lực ép xe xuống đường (Downforce)
        rb.AddForce(-transform.up * downforce * rb.linearVelocity.magnitude);

        // 3. Di chuyển và bẻ lái khi xe còn sống và đang chạm đất
        if (isCarGrounded && !isCrashed)
        {
            Movement();
            Turn();
        }
    }

    private void GetInput()
    {
        turnDirection = Input.GetAxis("Horizontal");
        if (isMoveLeft) turnDirection = -1f;
        else if (isMoveRight) turnDirection = 1f;
    }

    private void Movement()
    {
        RaycastHit hit;
        Vector3 moveDir = transform.forward;

        if (Physics.Raycast(transform.position, -transform.up, out hit, 3f, groundLayer))
        {
            moveDir = Vector3.ProjectOnPlane(transform.forward, hit.normal).normalized;
        }

        if (Math.Abs(turnDirection) >= 0.15f)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, speedturn, speedminturn * Time.fixedDeltaTime);
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, speed, acceleration * Time.fixedDeltaTime);
        }

        // XOAY XE ĐÚNG CHUẨN VẬT LÝ
        float rotateAmount = turnDirection * turnSpeed * Time.fixedDeltaTime;
        Quaternion deltaRotation = Quaternion.Euler(0, rotateAmount, 0);
        rb.MoveRotation(rb.rotation * deltaRotation);

        // DI CHUYỂN TÔN TRỌNG TRỌNG LỰC & LÒ XO
        Vector3 targetVelocity = moveDir * currentSpeed;
        targetVelocity.y = rb.linearVelocity.y; // Giữ lại lực rơi tự do và lực nâng của lò xo
        rb.linearVelocity = targetVelocity;

        // CHẶN BIÊN X ĐÚNG CHUẨN VẬT LÝ
        Vector3 currentPos = rb.position;
        if (currentPos.x > maxX || currentPos.x < -maxX)
        {
            currentPos.x = Mathf.Clamp(currentPos.x, -maxX, maxX);
            rb.position = currentPos;
        }
    }

    private void Turn()
    {
        float targetSteerAngle = turnDirection * maxSteerAngle;
        Quaternion targetWheelRotation = Quaternion.Euler(0, targetSteerAngle, 0);
        if (Math.Abs(turnDirection) >= 0.15f)
        {
            trail.SetActive(true);
            foreach (ParticleSystem smoke in smokes)
            {
                smoke.Emit(1);
            }
        }
        else
        {
            trail.SetActive(false);
        }

        if (frontLeftWheel != null) frontLeftWheel.localRotation = Quaternion.Lerp(frontLeftWheel.localRotation, targetWheelRotation, turnSpeed * Time.fixedDeltaTime);
        if (frontRightWheel != null) frontRightWheel.localRotation = Quaternion.Lerp(frontRightWheel.localRotation, targetWheelRotation, turnSpeed * Time.fixedDeltaTime);
    }

    // ========================================================
    // HỆ THỐNG GIẢM XÓC LÒ XO 4 BÁNH (SUSPENSION)
    // ========================================================
    private void ApplyHoverSuspension()
    {
        bool isFL = ApplySuspensionToWheel(frontLeftWheel);
        bool isFR = ApplySuspensionToWheel(frontRightWheel);
        bool isRL = ApplySuspensionToWheel(rearLeftWheel);
        bool isRR = ApplySuspensionToWheel(rearRightWheel);

        isCarGrounded = isFL || isFR || isRL || isRR;
    }

    private bool ApplySuspensionToWheel(Transform wheel)
    {
        if (wheel == null) return false;

        RaycastHit hit;
        if (Physics.Raycast(wheel.position, -transform.up, out hit, rayLength, groundLayer))
        {
            float currentDist = hit.distance;
            float compression = rideHeight - currentDist;

            // Nếu bánh xe lún sâu hơn độ cao cho phép (rideHeight), đẩy nó lên!
            if (compression > 0)
            {
                Vector3 wheelVelocity = rb.GetPointVelocity(wheel.position);
                float upVelocity = Vector3.Dot(wheelVelocity, transform.up);

                // Công thức tính lực lò xo kết hợp cản thủy lực (Giảm xóc)
                float force = (compression * suspensionForce) - (upVelocity * suspensionDamping);

                // Áp dụng lực hất lên tại chính xác vị trí của bánh xe đó
                rb.AddForceAtPosition(transform.up * Mathf.Max(0, force), wheel.position);
            }
            return true;
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (frontLeftWheel != null) Gizmos.DrawLine(frontLeftWheel.position, frontLeftWheel.position - transform.up * rayLength);
        if (frontRightWheel != null) Gizmos.DrawLine(frontRightWheel.position, frontRightWheel.position - transform.up * rayLength);
        if (rearLeftWheel != null) Gizmos.DrawLine(rearLeftWheel.position, rearLeftWheel.position - transform.up * rayLength);
        if (rearRightWheel != null) Gizmos.DrawLine(rearRightWheel.position, rearRightWheel.position - transform.up * rayLength);
    }

    public void UpPoiterLeft() { isMoveLeft = false; }
    public void UpPoiterRight() { isMoveRight = false; }
    public void DownPointerLeft() { isMoveLeft = true; }
    public void DownPointerRight() { isMoveRight = true; }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("obstacle") && !isCrashed)
        {
            isCrashed = true;
            camera_rb.isKinematic = true;
            trail.SetActive(false);

            CinemachineVirtualCamera vcam = FindAnyObjectByType<CinemachineVirtualCamera>();

            if (vcam != null)
            {
                Vector3 currentPos = new Vector3(0, 3.612015f, transform.position.z - 5.06311f);
                vcam.transform.position = currentPos;
                vcam.Follow = null;
                vcam.LookAt = null;
            }

            rb.linearDamping = slideFriction;
            Vector3 slideVelocity = transform.forward * (rb.linearVelocity.magnitude * 0.3f);
            rb.linearVelocity = slideVelocity;
            rb.AddForce(Vector3.up * popUpForce, ForceMode.Impulse);

            Rigidbody obs = collision.gameObject.GetComponent<Rigidbody>();
            if (obs != null)
            {
                obs.isKinematic = false;
                Vector3 knocback = (Vector3.up + Vector3.forward).normalized;
                obs.AddForce(knocback * obstacleKnockback, ForceMode.Impulse);
            }
        }
        Debug.Log("<color=orange>Va chạm cứng với: </color>" + collision.gameObject.name);
    }

    public bool IsEndGame()
    {
        return isCrashed;
    }
}