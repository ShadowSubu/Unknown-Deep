using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabArm : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Clam"))
        {
            WorldManager.instance.submarine.fragmentsCollected++;
            Destroy(other.gameObject);

            if (WorldManager.instance.submarine.fragmentsCollected >= 4)
            {
                WorldManager.instance.submarine.bladeUnlocked = true;
            }
            if (WorldManager.instance.submarine.fragmentsCollected >= 8)
            {
                WorldManager.instance.submarine.drillUnlocked = true;
            }
        }
    }
}
