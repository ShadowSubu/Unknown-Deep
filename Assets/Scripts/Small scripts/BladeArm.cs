using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeArm : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Vine") && WorldManager.instance.submarine.bladeArm.activeSelf == true)
        {
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
