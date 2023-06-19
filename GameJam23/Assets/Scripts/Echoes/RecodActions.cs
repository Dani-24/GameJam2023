using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecodActions : MonoBehaviour
{

  //  private GameObject player;
    private Player playerSc;
   // private GameObject gun;
    private Gun gunSc;
    List<PlayerEcoActions> actionsList;
    //bool isRecording;
    bool canRedcord = true;
    GameObject playerGun;
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerSc = player.GetComponent<Player>();
        playerGun = GameObject.FindGameObjectWithTag("PlayerGun");
        //gun = GameObject.FindGameObjectWithTag("Gun");
        gunSc = playerSc.GetComponent<Gun>();


        actionsList = new List<PlayerEcoActions>();
    }

    // Update is called once per frame
    void LateUpdate()
    {

        if (canRedcord)
        {
            if (playerSc.GetInputPlayer().x != 0 || playerSc.GetInputPlayer().y != 0 || gunSc.GetIsShooting()) 
            {
               // StartRecording();
                InSertOnList();
            }
            else
            {
               // StopRecording();
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

    public void ClearActions()
    {
        actionsList.Clear();
    }
    public List<PlayerEcoActions> CloneActions()
    {
       // clone = new List<PlayerEcoActions>(actionsList);
        return actionsList;
    }
    private void InSertOnList()
    {
        actionsList.Add( new PlayerEcoActions(playerSc.transform.position, playerGun.transform.rotation, gunSc.GetIsShooting()));
       // actionsList.Add( new PlayerEcoActions(playerSc.GetPlayerRB().position, playerGun.transform.rotation, gunSc.GetIsShooting()));
    }

    //void StartRecording()
    //{
    //    isRecording = true;
    //} 
    //void StopRecording()
    //{
    //    isRecording = false;
    //}

    public void SetCanRecord(bool state)
    {
        canRedcord = state;
    }

}
