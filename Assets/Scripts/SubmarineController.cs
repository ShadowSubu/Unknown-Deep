using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineController : MonoBehaviour
{
    private Rigidbody rb;

    public float subSpeed;
    public float turnSpeed;
    public float rotationSpeed;
    private float currentSpeed;

    private static Vector3 forward = new Vector3(0f,0f,0f);
    private static Vector3 backward = new Vector3(0f,179f,0f);
    private static Vector3 upward = new Vector3(-40f, 0f, 0f);
    private static Vector3 downward = new Vector3(40f, 0f, 0f);
    private static Quaternion f = Quaternion.Euler(forward.x, forward.y, forward.z);
    private static Quaternion b = Quaternion.Euler(backward.x, backward.y, backward.z);
    private static Quaternion u = Quaternion.Euler(upward.x, upward.y, upward.z);
    private static Quaternion d = Quaternion.Euler(downward.x, downward.y, downward.z);
    private float turnSmoothVelocity = 0.5f;
    private float turnSmoothTime = 0.1f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float horizontolInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector3 movement = new Vector3(0f, verticalInput, horizontolInput).normalized;

        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, f, rotationSpeed * Time.deltaTime);
            currentSpeed = subSpeed;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, b, rotationSpeed * Time.deltaTime);
            currentSpeed = subSpeed;
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0, 0.01f);
        }

        if (Input.GetKey(KeyCode.W))
        {
            Quaternion temp = Quaternion.Euler(upward.x, transform.rotation.eulerAngles.y, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, temp, rotationSpeed * Time.deltaTime);
            currentSpeed = subSpeed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Quaternion temp = Quaternion.Euler(downward.x, transform.rotation.eulerAngles.y, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, temp, rotationSpeed * Time.deltaTime);
            currentSpeed = subSpeed;
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0, 0.01f);
        }
        transform.position = new Vector3(6.64f, transform.position.y, transform.position.z);
        currentSpeed = Mathf.Clamp(currentSpeed, 0, subSpeed);
        rb.velocity = movement * currentSpeed;
    }
}
