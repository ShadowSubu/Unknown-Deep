using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillArm : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Blockade"))
        {
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
