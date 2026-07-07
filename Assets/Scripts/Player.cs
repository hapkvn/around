using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Cài đặt di chuyển")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float turnSpeed = 3f;

    [Header("Cài đặt bánh xe")]
    [SerializeField] private Transform frontLeftWheel;  
    [SerializeField] private Transform frontRightWheel;




    private Rigidbody rb;
    private float horizontalInput;
    private float verticalInput;
    private float maxSteerAngle = 30f;
    void Start()
    {
     rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Movement();
        Turn();
    }
    private void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        
        transform.Translate(Vector3.forward * speed * verticalInput * Time.fixedDeltaTime, Space.Self);

        float rotateAmount = horizontalInput* turnSpeed * Time.fixedDeltaTime;
        transform.Rotate(0, rotateAmount, 0);

       

    }

    private void Turn()
    {
        float horzitalInput = Input.GetAxis("Horizontal");

        float currentSteerAngle = horizontalInput * maxSteerAngle;
        

        if (frontLeftWheel != null)
        {
            frontLeftWheel.localRotation = Quaternion.Euler(0, currentSteerAngle, 0);
        }

        if (frontRightWheel != null)
        {
            frontRightWheel.localRotation = Quaternion.Euler(0, currentSteerAngle, 0);
        }
    }

   
}
