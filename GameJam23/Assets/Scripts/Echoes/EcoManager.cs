using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EcoManager : MonoBehaviour
{
    RecodActions actionToDo;
    Player player;
    [SerializeField]GameObject ecoPrefab;
    List<GameObject> ecoOnScene;
    public float starMovTime = 0.5f;
    // Start is called before the first frame update

    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        actionToDo = player.GetComponent<RecodActions>();

        ecoOnScene = new List<GameObject>();
    }

    // Update is called once per frame

    private void LateUpdate()
    {
        if ((player.GetIsDamaging() || Input.GetKeyDown(KeyCode.Space))&&player.clones > 0)
        {
            player.clones -= 1;
            player.isRedo = true;
            actionToDo.SetCanRecord(false);
            //GameObject eco = Instantiate(ecoPrefab, player.GetStartTransform().position, player.GetStartTransform().rotation);
            player.ResetPlayerPosOnLVL();
            
            if (player.GetIsDamaging()) player.SetIsDamaging(false);

        }
        if(player.endRedo)
        {
            
            player.endRedo = false;
            actionToDo.SetCanRecord(true);
            GameObject newEco = Instantiate(ecoPrefab, player.GetStartTransform().position, player.GetStartTransform().rotation);

            //foreach (var activeEco in ecoOnScene)
            //{
            //    //GameObject.find
            //}
            if (ecoOnScene.Count > 0)
            {
                for (int i = 0; i < ecoOnScene.Count; i++)
                {
                    ecoOnScene[i].SetActive(true);
                    ecoOnScene[i].GetComponent<PlayerEco>().RestartEcoPos();
                }

            }

            ecoOnScene.Add(newEco);
        }
    }

}
