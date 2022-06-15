using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(End());
        }
    }

    IEnumerator End()
    {
        GameManager.instance.isStart = false;
        WorldManager.instance.loader.transition.SetTrigger("start");
        yield return new WaitForSeconds(WorldManager.instance.loader.transitoinTime);
        GameManager.instance.ChangeState(GameState.gameOver);
    }
}
