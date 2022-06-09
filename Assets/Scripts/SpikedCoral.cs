using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpikedCoral : MonoBehaviour
{
    public float distance = 4;
    public float forwardDuration;
    public float backwardDuration;
    public float startTime;
    public float repeatRate;
    Vector3 position;


    void Start()
    {
        position = transform.position;
        InvokeRepeating(nameof(Move), startTime, repeatRate);
    }

    void Update()
    {
        
    }

    private void Move()
    {
        transform.DOLocalMove(new Vector3(transform.position.x, transform.position.y, transform.position.z + distance), forwardDuration).OnComplete(() =>
        {
            transform.DOLocalMove(position, backwardDuration);
        });
    }
}
