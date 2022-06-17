using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIAnimations : MonoBehaviour
{
    public void PopAnimation(RectTransform obj)
    {
        obj.DOShakeScale(0.2f, 0.2f, 2, 60f, true).OnComplete(() =>
        {
            obj.localScale = new Vector3(1f, 1f, 1f);
        });
    }
}
