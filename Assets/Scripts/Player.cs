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
        Movement();
        Turn();
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

    public void UpPoiterLeft() { isMoveLeft = false; }
    public void UpPoiterRight() { isMoveRight = false; }
    public void DownPointerLeft() { isMoveLeft = true; }
    public void DownPointerRight() { isMoveRight = true; }

}