using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private string nextLevelName;
    [SerializeField] private GameObject canvas;
    private Animator animator;

    [SerializeField] private Transform player;
    [SerializeField] private Transform spawn;

    private void Start()
    {
        animator = canvas.GetComponent<Animator>();
        player.position = spawn.position;
    }

    public void NextLevel() 
    {
        SceneManager.LoadScene(nextLevelName);
    }
}
