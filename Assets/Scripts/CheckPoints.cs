using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CheckPoints : MonoBehaviour
{
    [SerializeField] Collider biomeTwoCheckpoint;
    [SerializeField] Collider biomeThreeCheckpoint;
    [SerializeField] Transform biomeOneRespawnPoint;
    [SerializeField] Transform biomeTwoRespawnPoint;
    [SerializeField] Transform biomeThreeRespawnPoint;
    [SerializeField] RectTransform checkPointReachedText;

    bool checkpointOne = false;
    bool checkpointTwo = false;

    public void Respawn()
    {
        if (checkpointOne && checkpointTwo)
        {
            SetTransform(biomeThreeRespawnPoint);
        }
        else if (checkpointOne && !checkpointTwo)
        {
            SetTransform(biomeTwoRespawnPoint);
        }
        else if (!checkpointOne && !checkpointTwo)
        {
            SetTransform(biomeOneRespawnPoint);
        }
    }

    private void SetTransform(Transform target)
    {
        WorldManager.instance.submarine.transform.position = target.transform.position;
        WorldManager.instance.submarine.transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
        //WorldManager.instance.submarine.transform.Rotate(Vector3.zero);
    }

    public void ReachedCheckPointOne()
    {
        if (!checkpointOne)
        {
            checkpointOne = true;
            CheckPointAnim();
        }
    }

    public void ReachedCheckPointTwo()
    {
        if (!checkpointTwo)
        {
            checkpointTwo = true;
            CheckPointAnim();
        }
    }

    void CheckPointAnim()
    {
        checkPointReachedText.DOAnchorPos(new Vector2(2500f, 0f), 0f).OnComplete(() =>
        {
            checkPointReachedText.DOAnchorPos(new Vector2(0f, 0f), 0.5f).OnComplete(() =>
            {
                checkPointReachedText.DOAnchorPos(new Vector2(0f, 0f), 1f).OnComplete(() =>
                {
                    checkPointReachedText.DOAnchorPos(new Vector2(-2500f, 0f), 0.5f);
                });
            });
        });
    }
}
