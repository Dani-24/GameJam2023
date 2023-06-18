using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEco : MonoBehaviour
{
    List<PlayerEcoActions> actionsEco;
    RecodActions playerActions;
    Player playerGO;
    Quaternion gunRotZ;
    [SerializeField] private float disapearTime = 2f;
    private float _disapearDt = 0;
    // Start is called before the first frame update
    void Start()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerActions = GameObject.FindGameObjectWithTag("Player").GetComponent<RecodActions>();
        playerActions.CloneActions(actionsEco);
        


    } 

    // Update is called once per frame
    void Update()
    {
      
    }

    private void FixedUpdate()
    {
        if(actionsEco.Count>0)
        {
            PlayerEcoActions actions = actionsEco[0];
            transform.position = actions.playerTrans.position;
            gameObject.GetComponent<Gun>().SetEcoCanShoot(actions.isShoot);
            gunRotZ = actions.gunRot;
           // transform.rotation = actions.playerTrans.rotation;

        }
        else
        {
            DisapearEco();
        }
    }

  
    public Quaternion GetGunRotZ()
    {
        return gunRotZ;
    }

    public void DisapearEco()
    {
        _disapearDt += Time.deltaTime;
        if(_disapearDt>=disapearTime)
        {


            Destroy(gameObject);
        }


    }
}
