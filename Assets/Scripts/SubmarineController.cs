using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineController : MonoBehaviour
{
    private Rigidbody rb;

    public float subSpeed;
    public float turnSpeed;
    private float currentSpeed;

    private static Vector3 forward = new Vector3(0f,0f,0f);
    private static Vector3 backward = new Vector3(0f,180f,0f);
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

        Vector3 moveDir;
        float targetAngle = Mathf.Atan2(movement.z, movement.y) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.x, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(angle, 0f, 0f);

        moveDir = Quaternion.Euler(targetAngle, 0f, 0f) *Vector3.one;

        if (Input.GetKey(KeyCode.D))
        {
            //transform.rotation = Quaternion.Euler(forward);
            //currentSpeed = subSpeed;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            //transform.rotation = Quaternion.Euler(backward);
            //currentSpeed = subSpeed;
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0, 0.02f);
        }
        currentSpeed = Mathf.Clamp(currentSpeed, -subSpeed, subSpeed);
        rb.velocity = moveDir * subSpeed;
    }
}
