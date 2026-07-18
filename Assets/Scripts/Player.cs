using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Cài đặt di chuyển")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float turnSpeed = 10f; 
    private bool isMoveLeft = false;
    private bool isMoveRight = false;

    [Header("Cài đặt bánh xe")]
    [SerializeField] private Transform frontLeftWheel;
    [SerializeField] private Transform frontRightWheel;
    [SerializeField] private Transform rearLeftWheel;
    [SerializeField] private Transform rearRightWheel;

    [Header("Cài đặt mặt đất")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float rayLength = 0.5f;
    public bool isCarGrounded = false;

    private Rigidbody rb;
    private float maxSteerAngle = 30f;    
    private float maxX = 20f;

    
    private float turnDirection = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        GetInput();

        // Cập nhật trạng thái chạm đất liên tục
        CheckAllWheelsGrounded();

        // Chỉ cho phép xe chạy và bẻ lái khi có ít nhất 1 bánh (hoặc cả 4 bánh) chạm đất
        if (isCarGrounded)
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
        float rotateAmount = turnDirection * turnSpeed * Time.fixedDeltaTime;
        transform.Rotate(0, rotateAmount, 0);

     
        transform.Translate(Vector3.forward * speed * Time.fixedDeltaTime, Space.Self);

    
        Vector3 currentPos = transform.position;
        currentPos.x = Mathf.Clamp(currentPos.x, -maxX, maxX);
        transform.position = currentPos;
    }

    private void Turn()
    {
        float targetSteerAngle = turnDirection * maxSteerAngle;
        Quaternion targetWheelRotation = Quaternion.Euler(0, targetSteerAngle, 0);

        if (frontLeftWheel != null)
        {
            frontLeftWheel.localRotation = Quaternion.Lerp(frontLeftWheel.localRotation, targetWheelRotation, turnSpeed * Time.fixedDeltaTime);
        }

        if (frontRightWheel != null)
        {
            frontRightWheel.localRotation = Quaternion.Lerp(frontRightWheel.localRotation, targetWheelRotation, turnSpeed * Time.fixedDeltaTime);
        }
    }
    private void CheckAllWheelsGrounded()
    {
        // Kiểm tra từng bánh xem có chạm đất không
        bool isFLGrounded = CheckSingleWheel(frontLeftWheel);
        bool isFRGrounded = CheckSingleWheel(frontRightWheel);
        bool isRLGrounded = CheckSingleWheel(rearLeftWheel);
        bool isRRGrounded = CheckSingleWheel(rearRightWheel);

        // Xe được tính là chạm đất nếu ÍT NHẤT 1 trong 4 bánh chạm đất
        // (Nếu bạn muốn khắt khe hơn, bắt buộc cả 4 bánh chạm đất mới được chạy thì thay dấu || thành &&)
        isCarGrounded = isFLGrounded || isFRGrounded || isRLGrounded || isRRGrounded;
    }

    // Hàm bắn tia Raycast cho 1 bánh xe
    private bool CheckSingleWheel(Transform wheel)
    {
        if (wheel == null) return false;

        // Bắn tia từ vị trí bánh xe, hướng thẳng xuống (-transform.up)
        return Physics.Raycast(wheel.position, -transform.up, rayLength, groundLayer);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // Đổi màu tia thành đỏ

        if (frontLeftWheel != null) Gizmos.DrawLine(frontLeftWheel.position, frontLeftWheel.position - transform.up * rayLength);
        if (frontRightWheel != null) Gizmos.DrawLine(frontRightWheel.position, frontRightWheel.position - transform.up * rayLength);
        if (rearLeftWheel != null) Gizmos.DrawLine(rearLeftWheel.position, rearLeftWheel.position - transform.up * rayLength);
        if (rearRightWheel != null) Gizmos.DrawLine(rearRightWheel.position, rearRightWheel.position - transform.up * rayLength);
    }

    public void UpPoiterLeft() { isMoveLeft = false; }
    public void UpPoiterRight() { isMoveRight = false; }
    public void DownPointerLeft() { isMoveLeft = true; }
    public void DownPointerRight() { isMoveRight = true; }

}