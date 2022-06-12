using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitoinTime = 1f;

    public void LoadMenu()
    {
        StartCoroutine(LoadLevel(0));
    }

    public void LoadLevel()
    {
        StartCoroutine(LoadLevel(1));
    }

    IEnumerator LoadLevel(int buildIndex)
    {
        transition.SetTrigger("start");
        yield return new WaitForSeconds(transitoinTime);
        SceneManager.LoadScene(buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
