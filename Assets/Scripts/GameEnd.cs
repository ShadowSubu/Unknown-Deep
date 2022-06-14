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
        yield return new WaitForSeconds(1);
        GameManager.instance.ChangeState(GameState.gameOver);
    }
}
