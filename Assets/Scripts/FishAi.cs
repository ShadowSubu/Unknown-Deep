using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAi : MonoBehaviour
{
    private float radius;
    private Rigidbody rb;
    private bool isMoving;

    private float speed = 2f;
    private float rotspeed = 50f;
    private Vector3 direction;
    private Quaternion rotation;
    private static Quaternion f = Quaternion.Euler(0f, 0f, 0f);
    private static Vector3 backward = new Vector3(0f, 180f, 0f);
    private static Quaternion b = Quaternion.Euler(backward.x, backward.y, backward.z);

    private float[] dis = new float[4];

    private Vector3[] rayDirection = new Vector3[4];


    Vector3 targetPos;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        SetRandomDirection();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if (isMoving)
        {
            
            

            Vector3 movement = direction.normalized;
            rb.velocity = movement * speed;
        }
        else
        {
            Vector3 movement = (new Vector3(0, direction.y + 1f, direction.y + 1f)).normalized;
            rb.velocity = -movement * speed * 2f;
           
        }
    }

    void Rotation()
    {
        float roty;
        if(direction.z < 0f)
        {
            rotation = b;
        }
        else
        {
            rotation = f;

        }
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1f);

        float rotx = Mathf.Atan2(direction.y, direction.z) * Mathf.Rad2Deg * -1f;
        rotation = Quaternion.Euler(rotx, transform.rotation.eulerAngles.y, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1f);
    }

    void SetRandomDirection()
    {
        if (Physics.Raycast(transform.position, Vector3.forward, out RaycastHit hitInfo, 100f))
        {
            dis[0] = hitInfo.point.z - transform.position.z;
            rayDirection[0] = new Vector3(0f, 0f, 1f);

        }
        if (Physics.Raycast(transform.position, Vector3.back, out RaycastHit hitInfo1, 100f))
        {
            dis[1] = transform.position.z - hitInfo1.point.z;
            rayDirection[1] = new Vector3(0f, 0f, -1f);

        }
        if (Physics.Raycast(transform.position, Vector3.up, out RaycastHit hitInfo2, 100f))
        {
            dis[2] = hitInfo2.point.y - transform.position.y;
            rayDirection[2] = new Vector3(0f, 1f, 0f);

        }
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo3, 100f))
        {
            dis[3] = transform.position.y - hitInfo3.point.y;
            rayDirection[3] = new Vector3(0f, -1f, 0f);
        }
        float max = dis[0];
        int index = 0;
        for (int i = 0; i < dis.Length; i++)
        {
            if(max < dis[i])
            {
                index = i;
                max = dis[i];

            }
        }

        if(index == 0)
        {
            direction = rayDirection[0];

        }
        else if (index == 1)
        {
            direction = rayDirection[1];

        }
        else if (index == 2)
        {
            direction = rayDirection[2];

        }
        else if (index == 3)
        {
            direction = rayDirection[3];

        }
        else
        {
            direction = rayDirection[0];

        }
        print(index);
        print(direction);
        Rotation();



        isMoving = true;
    }



    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Level"))
    //    {
    //        print("iuopuioiuo");

    //        isMoving = true;
    //    }
    //}
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Level"))
        {
            print("vbnnbmbmvb");
            if (isMoving)
            {
                StartCoroutine(SetRandomDirectionCo());
            }

        }
    }

    IEnumerator SetRandomDirectionCo()
    {
        isMoving = false;

        yield return new WaitForSeconds(1f);
        if (!isMoving)
        {
            SetRandomDirection();
        }
        
    }
}
