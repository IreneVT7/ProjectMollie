using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMenuCanvasManager : MonoBehaviour
{
    public GameObject mainMenuCanvas;
    public GameObject settingsCanvas;
    public GameObject controlsCanvas;
    public AudioClip uisound;
    public bool isMainMenu;


    // Start is called before the first frame update
    void Start()
    {
        if (isMainMenu == true)
        {
            mainMenuCanvas.SetActive(true);
            settingsCanvas.SetActive(false);
        }
        else
        {
            mainMenuCanvas.SetActive(false);
            settingsCanvas.SetActive(false);
            controlsCanvas.SetActive(false);
        }
    }

    public void MainMenuEnable()
    {
        // SoundManager.instance.Play(uisound);
        try
        {
            mainMenuCanvas.SetActive(true);
            settingsCanvas.SetActive(false);
            controlsCanvas.SetActive(false);
        }
        catch
        {

        }
    }

    public void SettingsEnable()
    {
        // SoundManager.instance.Play(uisound);
        try
        {
            mainMenuCanvas.SetActive(false);
            settingsCanvas.SetActive(true);
            controlsCanvas.SetActive(false);
        }
        catch
        {

        }

    }

    public void ControlsEnable()
    {
        // SoundManager.instance.Play(uisound);
        try
        {
            mainMenuCanvas.SetActive(false);
            settingsCanvas.SetActive(false);
            controlsCanvas.SetActive(true);
        }
        catch
        {

        }

    }

}
