using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameState gameState;
    public bool isStart = true;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {

    }

    public void ChangeState(GameState newState)
    {
        gameState = newState;
        switch (newState)
        {
            case GameState.gameStart:
                break;
            case GameState.respawn:
                WorldManager.instance.checkPoint.Respawn();
                break;
            case GameState.levelSuccess:
                break;
            case GameState.gameOver:
                StartCoroutine(GameEnd());
                break;
            default:
                break;
        }
    }
    IEnumerator GameEnd()
    {
        WorldManager.instance.loader.transition.SetTrigger("start");
        yield return new WaitForSeconds(WorldManager.instance.loader.transitoinTime);
        SceneManager.LoadScene(1);
    }
}


public enum GameState
{
    gameStart,
    respawn,
    levelSuccess,
    gameOver
}

