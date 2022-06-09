using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameState gameState;

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
                break;
            case GameState.levelSuccess:
                break;
            case GameState.gameOver:
                break;
            default:
                break;
        }
    }
}

public enum GameState
{
    gameStart,
    respawn,
    levelSuccess,
    gameOver
}

