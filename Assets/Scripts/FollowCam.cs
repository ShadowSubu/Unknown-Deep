using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform target;
    public float moveSmoothing;
    public float rotationSmoothing;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = target.position;
        transform.rotation = target.rotation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, moveSmoothing);
        transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, rotationSmoothing);
    }
}
