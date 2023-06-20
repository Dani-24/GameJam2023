using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    [Header("Win Condition")]
    [SerializeField] bool killAllEnemies = true;

    [Header("Required Components")]
    [SerializeField] GameObject nextLvlTp;

    [Header("Debug Info")]
    [SerializeField]
    GameObject[] EnemiesInThisScene;

    [SerializeField]
    int enemyCount = 0;

    bool allEnemiesDead;

    void Start()
    {
        nextLvlTp.SetActive(false);

        EnemiesInThisScene = GameObject.FindGameObjectsWithTag("Enemy");

        allEnemiesDead = false;
    }

    void Update()
    {
        // Ganar
        if(killAllEnemies && allEnemiesDead)
        {
            Win();
        }

        // Contar enemigos en la scene
        enemyCount = 0;
        for (int i = 0; i < EnemiesInThisScene.Length; i++)
        {
            if (EnemiesInThisScene[i].activeInHierarchy == true)
            {
                enemyCount++;
            }
        }
        if(enemyCount <= 0)
        {
            allEnemiesDead = true;
        }
    }

    void Win()
    {
        nextLvlTp.SetActive(true);
    }
}
