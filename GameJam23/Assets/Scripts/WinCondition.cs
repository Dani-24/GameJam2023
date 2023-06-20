using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    [Header("Win Condition")]
    [SerializeField] LvlConditions conditions = LvlConditions.None;

    [Header("Required Components")]
    [SerializeField] GameObject nextLvlTp;

    [Header("If Activate Things selected")]
    [SerializeField]
    List<GameObject> thingsToActivate;

    [Header("Debug Info")]
    [SerializeField]
    GameObject[] EnemiesInThisScene;

    [SerializeField]
    int enemyCount = 0;

    [SerializeField]
    int activablesCount = 0;

    [SerializeField]
    bool allEnemiesDead;

    [SerializeField]
    bool allActivated;

    void Start()
    {
        nextLvlTp.SetActive(false);

        EnemiesInThisScene = GameObject.FindGameObjectsWithTag("Enemy");

        allEnemiesDead = allActivated = false;
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
                if(allActivated) { Win(); }
                break;
            case LvlConditions.Both:
                if(allEnemiesDead && allActivated) { Win(); }
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

        // Check activables
        activablesCount = 0;
        for(int i = 0; i < thingsToActivate.Count; i++)
        {
            if (thingsToActivate[i].GetComponent<InteractuableItem>().activated)
            {
                activablesCount++;
            }
        }
        if(activablesCount >= thingsToActivate.Count)
        {
            allActivated = true;
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
        Both
    }
}
