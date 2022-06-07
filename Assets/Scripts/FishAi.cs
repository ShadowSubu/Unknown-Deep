using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAi : MonoBehaviour
{
    private float radius;
    private Rigidbody rb;
    private bool isMoving;
    Vector3 targetPos;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        
    }

    void Move()
    {
        if (isMoving)
        {

        }
    }

    void SetRandomTarget()
    {
        if (Physics.Raycast(transform.position, Vector3.forward, radius, 8))
        {

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Level"))
        {
            isMoving = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Level"))
        {
            isMoving = true;
        }
    }
}
