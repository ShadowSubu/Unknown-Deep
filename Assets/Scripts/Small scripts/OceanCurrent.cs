using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanCurrent : MonoBehaviour
{
    BoxCollider boxCollider;
    public float resetTime;
    public int force;
    SubmarineController controller;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //controller = other.GetComponent<SubmarineController>();
            //controller.canMove = false;
            //controller.GetComponent<Rigidbody>().useGravity = true;
            
            other.gameObject.GetComponent<Rigidbody>().AddForce(transform.TransformDirection(Vector3.forward) * force, ForceMode.Impulse);
        }
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(resetTime);
        controller.canMove = true;
        controller.GetComponent<Rigidbody>().useGravity = false;
    }
}
