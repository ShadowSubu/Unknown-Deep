using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GrabArm : MonoBehaviour
{
    [SerializeField] RectTransform bladeToolUnlockText;
    [SerializeField] RectTransform drillToolUnlockText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Clam"))
        {
            WorldManager.instance.submarine.fragmentsCollected++;
            WorldManager.instance.submarine.fragmentsText.text = WorldManager.instance.submarine.fragmentsCollected.ToString();
            WorldManager.instance.fragmentCollectionSound.Play();
            Destroy(other.gameObject);

            if (WorldManager.instance.submarine.fragmentsCollected == 4)
            {
                WorldManager.instance.submarine.bladeUnlocked = true;
                ToolUnlocked(bladeToolUnlockText);
            }
            if (WorldManager.instance.submarine.fragmentsCollected == 8)
            {
                WorldManager.instance.submarine.drillUnlocked = true;
                ToolUnlocked(drillToolUnlockText);
            }
        }
    }

    void ToolUnlocked(RectTransform text)
    {
        text.DOAnchorPos(new Vector2(2500f, 0f), 0f).OnComplete(() =>
        {
            text.DOAnchorPos(new Vector2(0f, 0f), 0.5f).OnComplete(() =>
            {
                text.DOAnchorPos(new Vector2(0f, 0f), 1f).OnComplete(() =>
                {
                    text.DOAnchorPos(new Vector2(-2500f, 0f), 0.5f);
                });
            });
        });
    }
}
