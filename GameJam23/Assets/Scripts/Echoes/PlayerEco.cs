using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEco : MonoBehaviour
{
    List<PlayerEcoActions> actionsEco;
    List<PlayerEcoActions> actionsEcoCpy;//Copy
    RecodActions playerActions;
   //Player playerGO;
    Quaternion gunRotZ;
    [SerializeField] private float disapearTime = 2f;
    private float _disapearDt = 0;
    [SerializeField] private int actions;
    Player playerSc;
    Vector2 playerInputEco;
    Vector2 mousePosEco;
    [SerializeField]private GameObject disapearParticle;
    // Start is called before the first frame update
    void Start()
    {
        playerSc = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerActions = playerSc.GetComponent<RecodActions>();
        //actionsEco = playerActions.CloneActions();
        actionsEco = new List<PlayerEcoActions>(playerActions.CloneActions());
        actionsEcoCpy = new List<PlayerEcoActions>(playerActions.CloneActions()); ;
        playerActions.ClearActions();

        actions = actionsEcoCpy.Count;
        
    }
    
    // Update is called once per frame
    void Update()
    {
        //if(playerSc.isRedo)
        //{
        //    DisapearEco();
        //}
        if (actionsEco.Count > 0 && !playerSc.isRedo)
        {
            PlayerEcoActions actions = actionsEco[0];
            transform.position = actions.playerTrans;
            gameObject.GetComponent<Gun>().SetEcoCanShoot(actions.isShoot);
            gunRotZ = actions.gunRot;
            playerInputEco = actions.input;
            mousePosEco = actions.mousePos;
            actionsEco.RemoveAt(0);
            // Debug.Log("a");
            // transform.rotation = actions.playerTrans.rotation;

        }
        else
        {
            DisapearEco();
            Debug.Log("obama");
        }
    }

    //private void FixedUpdate()
    //{
    //    if(actionsEco.Count>0)
    //    {
    //        PlayerEcoActions actions = actionsEco[0];
    //        transform.position = actions.playerTrans;
    //        gameObject.GetComponent<Gun>().SetEcoCanShoot(actions.isShoot);
    //        gunRotZ = actions.gunRot;
    //        actionsEco.RemoveAt(0);
    //       // Debug.Log("a");
    //       // transform.rotation = actions.playerTrans.rotation;

    //    }
    //    else
    //    {
    //        DisapearEco();
    //        Debug.Log("obama");
    //    }
    //}

  
    public Quaternion GetGunRotZ()
    {
        return gunRotZ;
    }

    public void DisapearEco()
    {
        _disapearDt += Time.deltaTime;
        disapearParticle.SetActive(true);
        if (_disapearDt>=disapearTime)
        {
            _disapearDt = 0;
            disapearParticle.SetActive(false);
            gameObject.SetActive(false);
            // RestartEcoPos();
            //Destroy(gameObject);
        }


    }
    public void RestartEcoPos()
    {
        Debug.Log("aaaa");
        // gameObject.SetActive(true);
        actionsEco.Clear();
        actionsEco = new List<PlayerEcoActions>(actionsEcoCpy);
    }
    public Vector2 GetPlayerInputEco()
    {

        return playerInputEco;
    }

    public Vector2 GetMousePosEco()
    {
        return mousePosEco;
    }

}
