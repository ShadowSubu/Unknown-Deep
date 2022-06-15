using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volcano : MonoBehaviour
{
    public float pauseInterval;
    public float playInterval;
    ParticleSystem volcanoParticle;
    BoxCollider boxCollider;

    private void Start()
    {
        volcanoParticle = GetComponent<ParticleSystem>();
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false;
        InvokeRepeating(nameof(PlayVolcano), 1f, playInterval);
    }

    void PlayVolcano()
    {
        volcanoParticle.Play();
        StartCoroutine(StopVolcano());
    }

    IEnumerator StopVolcano()
    {
        yield return new WaitForSeconds(0.3f);
        boxCollider.enabled = true;
        yield return new WaitForSeconds(pauseInterval);
        volcanoParticle.Stop();
        yield return new WaitForSeconds(1.5f);
        boxCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<SubmarineController>().TakeDamage(WorldManager.instance.volcanoDamage);
        }
    }
}
