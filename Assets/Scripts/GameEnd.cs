using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
using UnityEngine.Experimental.GlobalIllumination;

public class GameEnd : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] CinemachineVirtualCamera cine;
    [SerializeField] Transform creatureTarget;
    [SerializeField] GameObject gameEndPanel;

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
        MoveCamera();
        yield return new WaitForSeconds(1.5f);
        WorldManager.instance.ShowTimer();
        gameEndPanel.SetActive(true);
    }

    void MoveCamera()
    {
        cam.GetComponentInChildren<Light>().enabled = false;
        cine.enabled = false;
        cam.transform.DOMove(new Vector3(cam.transform.position.x, creatureTarget.position.y, creatureTarget.position.z), 1.5f).SetEase(Ease.InOutExpo);
    }

    public void GameOver()
    {
        GameManager.instance.ChangeState(GameState.gameOver);
    }
}
