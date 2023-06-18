using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecodActions : MonoBehaviour
{

    private GameObject player;
    private Player playerSc;
    private GameObject gun;
    private Gun gunSc;
    List<PlayerEcoActions> actionsList;
    bool isRecording;
    bool canRedcord = true;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerSc = player.GetComponent<Player>();

        gun = GameObject.FindGameObjectWithTag("Gun");
        gunSc = gun.GetComponent<Gun>();

        actionsList = new List<PlayerEcoActions>();
    }

    // Update is called once per frame
    void PostUpdate()
    {

        if (canRedcord)
        {
            if (playerSc.GetInputPlayer().x != 0 || playerSc.GetInputPlayer().y != 0 || gunSc.GetIsShooting()) 
            {
                StartRecording();
                InSertOnList();
            }
            else
            {
                StopRecording();
            } 
        }

       // ClearActions();


        //if(gunSc.GetIsShooting())
        //{
        //    Recording();
        //}
        //else
        //{
        //    StopRecording();
        //}
    }

    private void ClearActions()
    {
        actionsList.Clear();
    }
    public void CloneActions(List<PlayerEcoActions> clone)
    {
        clone = new List<PlayerEcoActions>(actionsList);
    }
    private void InSertOnList()
    {
        actionsList.Add( new PlayerEcoActions(playerSc.transform, gunSc.transform, gunSc.GetIsShooting()));
    }

    void StartRecording()
    {
        isRecording = true;
    } 
    void StopRecording()
    {
        isRecording = false;
    }


}
