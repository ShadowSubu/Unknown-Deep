using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillArm : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Blockade") && WorldManager.instance.submarine.drillArm.activeSelf == true)
        {
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
            Destroy(other.gameObject);
        }
    }
}
