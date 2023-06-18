using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EcoManager : MonoBehaviour
{
    RecodActions actionToDo;
    Player player;
    GameObject ecoPrefab;
    // Start is called before the first frame update

    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        actionToDo = player.GetComponent<RecodActions>();
    }

    // Update is called once per frame

    private void LateUpdate()
    {
        if (player.GetIsDamaging() || Input.GetKeyDown(KeyCode.Space))
        {
            actionToDo.SetCanRecord(false);
            GameObject eco = Instantiate(ecoPrefab, player.GetStartTransform().position, player.GetStartTransform().rotation);
            if (player.GetIsDamaging()) player.SetIsDamaging(false);

        }

    }

}
