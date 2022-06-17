using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmAction : MonoBehaviour
{
    [SerializeField] SubmarineController submarine;
    [SerializeField] Transform targetBone;
    Vector3 defaultPosition;
    Vector3 collisionPoint;
    bool reset;
    bool move = false;

    private void Start()
    {
        defaultPosition = targetBone.transform.localPosition;
    }

    private void OnTriggerStay(Collider other)
    {
        if (submarine.grabArm.gameObject.activeSelf == true && other.CompareTag("Clam"))
        {
            reset = false;
            move = true;
            collisionPoint = other.ClosestPoint(transform.position);
        }
        else
        {
            move = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (submarine.grabArm.gameObject.activeSelf == true && other.CompareTag("Clam"))
        {
        }
        reset = true;
    }

    private void Update()
    {
        GoToTarget();
        if (Input.GetKey(KeyCode.Space))
        {
        }

        if (reset)
        {
            ResetTarget();
        }
    }

    private void GoToTarget()
    {
        if (move)
        {
            targetBone.transform.position = Vector3.Lerp(targetBone.transform.position, collisionPoint, 0.1f);
        }
    }

    private void ResetTarget()
    {
        targetBone.transform.localPosition = Vector3.Lerp(targetBone.transform.localPosition, defaultPosition, 0.1f);
        if (reset)
        {
        }
    }
}
