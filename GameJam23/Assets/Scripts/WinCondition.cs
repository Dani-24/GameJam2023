using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    [Header("Win Condition")]
    [SerializeField] LvlConditions conditions = LvlConditions.None;

    [Header("Required Components")]
    [SerializeField] GameObject nextLvlTp;

    [Header("Debug Info")]
    [SerializeField]
    GameObject[] EnemiesInThisScene;

    [SerializeField]
    int enemyCount = 0;

    bool allEnemiesDead;

    List<bool> thingsToActivate;

    void Start()
    {
        nextLvlTp.SetActive(false);

        EnemiesInThisScene = GameObject.FindGameObjectsWithTag("Enemy");

        allEnemiesDead = false;
    }

    void Update()
    {
        // Saber Ganar
        switch (conditions)
        {
            case LvlConditions.None:
                Win();
                break;
            case LvlConditions.KillAllEnemies:
                if(allEnemiesDead) { Win(); }
                break;
            case LvlConditions.ActivateThings:
                break;
            case LvlConditions.GetToThePoint: 
                break;
            case LvlConditions.KillAndActivateThings: 
                break;
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

    enum LvlConditions
    {
        None,
        KillAllEnemies,
        ActivateThings,
        GetToThePoint,
        KillAndActivateThings,
    }
}
