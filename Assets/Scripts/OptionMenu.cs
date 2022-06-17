using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseMenu();
        }
    }

    public void SetVolume(Slider volume)
    {
        //audioMixer.SetFloat("volume", volume.value);
        AudioListener.volume = volume.value;
    }

    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void CloseMenu()
    {
        gameObject.SetActive(false);
    }
}
