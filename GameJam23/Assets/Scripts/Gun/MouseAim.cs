using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAim : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector2 mousePos;
    private Camera cam;
    private GameObject player;
    private Player playerSc;
    private Rigidbody2D playerRB;
    //private Vector3 startScl;
    private PlayerEco ecoAim;
    void Start()
    {
        if(gameObject.tag == "PlayerGun")
        {
            cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            player = GameObject.FindGameObjectWithTag("Player");
            playerRB = player.GetComponent<Rigidbody2D>();
            playerSc = player.GetComponent<Player>();
        }
        if(gameObject.tag=="EcoGun")
        {
            ecoAim = transform.parent.parent.gameObject.GetComponent<PlayerEco>();
        }
    }

    void Update()
    {
        if (gameObject.tag == "PlayerGun") mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    
    private void FixedUpdate()
    {
        float rotZ;
        if (gameObject.tag == "PlayerGun")
        {

            if (!playerSc.isRedo)
            {
                Vector2 lookDir = mousePos - new Vector2(playerRB.position.x, playerRB.position.y);
                rotZ = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f; 
                transform.rotation = Quaternion.Euler(0,0,rotZ);
            }
            
        }
       
        
        
    }

    private void LateUpdate()
    {
        if (gameObject.tag == "EcoGun")
        {
            transform.rotation = ecoAim.GetGunRotZ();
        }
        else if (gameObject.tag == "PlayerGun")
        { 
            if (playerSc.isRedo)
            {
                transform.rotation = playerSc.GetPlayerGunRotZ();
                
            }
        }
    }
}
