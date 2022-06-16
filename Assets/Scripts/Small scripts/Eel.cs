using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eel : MonoBehaviour
{
    BoxCollider boxCollider;
    public float resetTime;
    public int force;
    SubmarineController controller;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (controller == null)
            {
                controller = WorldManager.instance.submarine;
            }
            other.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.down * force, ForceMode.Impulse);
            controller.canMove = false;
            StartCoroutine(Reset());
        }
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(resetTime);
        controller.canMove = true;
    }
}
