using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeArm : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Vine"))
        {
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
