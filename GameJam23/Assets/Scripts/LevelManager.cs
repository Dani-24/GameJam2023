using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private string nextLevelName;
    [SerializeField] private GameObject canvas;

    private Animator animator;

    [SerializeField] Animator redoAnimator;

    private GameObject player;
    [SerializeField] private Transform spawn;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");

        animator = canvas.GetComponent<Animator>();
        player.transform.position = spawn.position;
    }

    public void NextLevel() 
    {
        SceneManager.LoadScene(nextLevelName);
    }

    private void Update()
    {
        if(player != null)
        {
            redoAnimator.SetBool("redo", player.GetComponent<Player>().isRedo);
        }
    }
}
