using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class DialogueSystem : MonoBehaviour
{
    public List<string> startingLines = new List<string>();
    public List<string> endingLines = new List<string>();

    [SerializeField] TextMeshProUGUI bubbleText;
    [SerializeField] VideoClip startingVideo;
    [SerializeField] VideoClip endingVideo;

    [SerializeField] VideoPlayer videoPlayer;
    int lineNumber = 0;

    private void Awake()
    {
        if (!GameManager.instance.isStart)
        {
            videoPlayer.clip = endingVideo;
        }
    }

    private void Start()
    {
        if (GameManager.instance.isStart)
        {
            bubbleText.text = startingLines[0];
        }
        else
        {
            bubbleText.text = endingLines[0];
        }
    }

    public void Next()
    {
        if (startingLines.Count >= lineNumber && GameManager.instance.isStart)
        {
            lineNumber++;
            if (lineNumber > startingLines.Count - 1)
            {
                SceneManager.LoadScene(2);
            }
            else
            {
                bubbleText.text = startingLines[lineNumber];
            }
        }

        if (!GameManager.instance.isStart)
        {
            Application.Quit();
        }
    }

    public void GotoEndScene()
    {
        GameManager.instance.isStart = false;
        videoPlayer.clip = endingVideo;
    }
}