using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeArm : MonoBehaviour
{
    [SerializeField] GameObject rotatingPart;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Vine") && WorldManager.instance.submarine.bladeArm.activeSelf == true)
        {
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void Update()
    {
        rotatingPart.transform.Rotate(Vector3.forward, -2f);
    }
}
