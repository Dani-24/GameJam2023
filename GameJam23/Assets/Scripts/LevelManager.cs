using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private string nextLevelName;
    [SerializeField] private GameObject canvas;

    private Animator animator;

    [SerializeField] Animator redoAnimator;

    private GameObject player;
    [SerializeField] private Transform spawn;

    [Header("UI")]
    [SerializeField] Slider restartSlider;
    [SerializeField] Slider skipSlider;

    [SerializeField] KeyCode restartKey;
    [SerializeField] KeyCode skipKey;

    [SerializeField] float slidersSpeed = 1.0f;

    [SerializeField] FinishLevel endLvl;

    bool restarting = false;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");

        animator = canvas.GetComponent<Animator>();
        player.transform.position = spawn.position;

        restartSlider.value = skipSlider.value = 0;

        restarting = false;
    }

    public void NextLevel() 
    {
        if (!restarting)
        {
            SceneManager.LoadScene(nextLevelName);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void Update()
    {
        if(player != null)
        {
            redoAnimator.SetBool("redo", player.GetComponent<Player>().isRedo);
        }

        if (Input.GetKey(restartKey))
        {
            if (restartSlider.value < 1)
            {
                restartSlider.value += Time.deltaTime * slidersSpeed;
            }
            else
            {
                endLvl.EndLevel();
            }
        }
        else
        {
            if (restartSlider.value > 0)
            {
                restartSlider.value -= Time.deltaTime * slidersSpeed / 2;
            }
        }

        if (Input.GetKey(skipKey))
        {
            if (skipSlider.value < 1)
            {
                skipSlider.value += Time.deltaTime * slidersSpeed;
            }
            else
            {
                endLvl.EndLevel();
            }
        }
        else
        {
            if (skipSlider.value > 0) { 
                skipSlider.value -= Time.deltaTime * slidersSpeed / 2;
            }
        }

        if(restartSlider.value != 0)
        {
            restarting = true;
        }
        else
        {
            restarting = false;
        }
    }
}
