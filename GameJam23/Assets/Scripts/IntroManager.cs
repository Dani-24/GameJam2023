using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    [SerializeField]
    GameObject mainCanvas;
    [SerializeField]
    GameObject settingsCanvas;

    AudioSource source;

    public AudioClip nextLvlSFX;
    public AudioClip settingsSFX;
    public AudioClip backToTitleSFX;

    public void StartGame()
    {
        source.pitch = 0.9f;
        source.clip = nextLvlSFX;
        source.Play();
        SceneManager.LoadScene("Level 1");
    }

    public void OpenSettings()
    {
        source.clip = settingsSFX;
        source.Play();

        mainCanvas.SetActive(false);
        settingsCanvas.SetActive(true);
    }

    public void CloseSettings()
    {
        source.clip = backToTitleSFX;
        source.Play();

        settingsCanvas.SetActive(false);
        mainCanvas.SetActive(true);
    }

    public void ExitGame() 
    {
        Application.Quit();
    }

    private void Start()
    {
        source = GetComponent<AudioSource>();
        source.pitch = 1.0f;
    }
}
