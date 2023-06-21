using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiInformation : MonoBehaviour
{
    [SerializeField] TMP_Text titleText;
    [SerializeField] TMP_Text contadorIntentos;

    Player playerScript;

    bool isSettingsOpen;

    [SerializeField] GameObject settingsCanvas;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        titleText.text = SceneManager.GetActiveScene().name;

        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        isSettingsOpen = false;
    }

    
    void Update()
    {
        if(playerScript != null)
        {
            contadorIntentos.text = playerScript.clones.ToString();
        }

        if(Input.GetKeyUp(KeyCode.Escape))
        {
            ToggleSettings();
        }
    }

    public void ToggleSettings()
    {
        if (!isSettingsOpen)
        {
            settingsCanvas.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            audioSource.Play();

            settingsCanvas.SetActive(false);
            Time.timeScale = 1;
        }

        isSettingsOpen = !isSettingsOpen;
    }

    public void GoToTitle()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Intro");
    }
}
