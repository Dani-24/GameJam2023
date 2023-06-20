using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float vel = 2;
    private Rigidbody2D playerRB;
    private Vector2 input;
    [SerializeField]private int lives=3;
    [SerializeField]private int life=1;
    private bool isDamaging = false;
    [SerializeField] private Transform startTrans;
    [SerializeField] public int clones = 2;
    [SerializeField] public GameObject redoTrail;
    private Vector3 starto;
    public bool isRedo = false;
    public bool endRedo = false;
    List<PlayerEcoActions> redoPlayercpy;
    RecodActions playerActions;
    Quaternion gunRotZ;
    Vector2 playerInput;
    Vector2 mousePos;
    //List<PlayerEcoActions> actionsList;

    [SerializeField]
    List<string> dieByTheseTags = new List<string>();

    void Start()
    {
        playerRB = gameObject.GetComponent<Rigidbody2D>();
        startTrans = transform;
        playerActions = gameObject.GetComponent<RecodActions>();
        starto = startTrans.position;
    }

    // Update is called once per frame
    void Update()
    {
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");
        //if(input.x>0 || input.y>0)
        //{

        //}
        if (Input.GetKeyDown(KeyCode.E))
        {
            transform.position = starto;
        }
        if(isRedo)
        {
            redoTrail.SetActive(true);
            RedoPlayer();
        }
        
    }
    private void FixedUpdate()
    {
        playerRB.velocity = new Vector2(input.x * vel, input.y * vel);
    }

    public Vector2 GetInputPlayer()
    {

        return input;
    }

    public void TakeDamage(int dmg)
    {
        Debug.Log("Furro q veo Furro q pateo");

        life -= dmg;
        lives -= 1;
        if(life<=0 )
        {
            if(lives<=0)
            {

                //Die();
                
            }
            else
            {
                isDamaging = true;
            }
        }
        

    }

    public bool GetIsDamaging()
    {
        return isDamaging;
    }
    
    public void SetIsDamaging(bool _isDmg)
    {
         isDamaging =_isDmg;
    }

    public Transform GetStartTransform()
    {
        return startTrans;
    }

    public void ResetPlayerPosOnLVL()
    {

        playerRB.Sleep();
        //transform.position = starto;
        SetRedoActions();
        playerRB.WakeUp();
       // endRedo = true;

        Debug.Log("hqnEIGHJIWEJGHIWEFBJUHFBWeuhbijgfn");
    } 
    public Rigidbody2D GetPlayerRB()
    {
        return playerRB;
    } 
    public void SetRedoActions()
    {
        redoPlayercpy = new List<PlayerEcoActions>(playerActions.CloneActions());
        redoPlayercpy.Reverse();
    }

    public void RedoPlayer()
    {
        if (redoPlayercpy.Count > 0)
        {
           
            PlayerEcoActions actions = redoPlayercpy[0];
            transform.position = actions.playerTrans;
            //gameObject.GetComponent<Gun>().SetEcoCanShoot(actions.isShoot);
            gunRotZ = actions.gunRot;
            playerInput = actions.input;
            mousePos = actions.mousePos;
            redoPlayercpy.RemoveAt(0);
            // Debug.Log("a");
            // transform.rotation = actions.playerTrans.rotation;

        }
        else
        {
            // DisapearEco();
            redoTrail.SetActive(false);
            isRedo = false;
            endRedo = true;
            Debug.Log("obama");
        }
    }

    public Quaternion GetPlayerGunRotZ()
    {
        return gunRotZ;
    }
    public Vector2 GetPlayerInput()
    {

        return input;
    }
    public Vector2 GetPlayerRedoInput()
    {

        return playerInput;
    }
    
    public Vector2 GetMousePos()
    {
        return mousePos;
    }

    // --- Detectar Collisiones q pegan al player ---

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach(var tag in dieByTheseTags)
        {
            if(collision.gameObject.tag == tag)
            {
                TakeDamage(1);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (var tag in dieByTheseTags)
        {
            if (collision.tag == tag)
            {
                TakeDamage(1);
            }
        }
    }
}
