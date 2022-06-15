using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitoinTime = 1f;
    [SerializeField] OptionMenu option;

    private void Start()
    {
        if (WorldManager.instance != null)
        {
            WorldManager.instance.loader = this;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            option.gameObject.SetActive(true);
        }
    }

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
