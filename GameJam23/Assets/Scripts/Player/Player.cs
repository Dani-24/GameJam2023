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
    [SerializeField] private int clones = 2;
    //List<PlayerEcoActions> actionsList;
    void Start()
    {
        playerRB = gameObject.GetComponent<Rigidbody2D>();
        startTrans = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
         input.x = Input.GetAxis("Horizontal");
         input.y = Input.GetAxis("Vertical");
        //if(input.x>0 || input.y>0)
        //{

        //}

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
}
