using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DrillArm : MonoBehaviour
{
    [SerializeField] GameObject target;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Blockade") && WorldManager.instance.submarine.drillArm.activeSelf == true)
        {
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
            Destroy(other.gameObject);
        }
    }

    private void OnEnable()
    {
        //InvokeRepeating(nameof(DrillAction), 1f, 1f);
    }

    public void DrillAction()
    {
        target.transform.DOLocalMove((target.transform.localPosition + new Vector3(0f,0f,-0.5f)), 0.5f).OnComplete(() =>
        {
            target.transform.DOLocalMove((target.transform.localPosition + new Vector3(0f, 0f, 0.5f)), 0.5f);
        });
    }

    public void CancelAnimation()
    {
        CancelInvoke();
    }
}
