using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiInformation : MonoBehaviour
{
    public string currentLvl;

    [SerializeField] TMP_Text titleText;
    [SerializeField] TMP_Text contadorIntentos;

    Player playerScript;

    void Start()
    {
        titleText.text = currentLvl;

        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    
    void Update()
    {
        if(playerScript != null)
        {
            contadorIntentos.text = playerScript.clones.ToString();
        }
    }
}
