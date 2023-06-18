using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAim : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector2 mousePos;
    private Camera cam;
    private GameObject player;
    private Rigidbody2D playerRB;
    private Vector3 startScl;
    private PlayerEco ecoAim;
    void Start()
    {
        if(gameObject.tag == "PlayerGun")
        {
            cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            player = GameObject.FindGameObjectWithTag("Player");
            playerRB = player.GetComponent<Rigidbody2D>();
        }
        if(gameObject.tag=="EcoGun")
        {
            ecoAim = transform.parent.gameObject.GetComponent<PlayerEco>();
        }
    }

    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    
    private void FixedUpdate()
    {
        float rotZ;
        if (gameObject.tag == "PlayerGun")
        {

            Vector2 lookDir = mousePos - new Vector2(playerRB.position.x, playerRB.position.y);
            // Vector2 lookDir = mousePos - new Vector2 (player.transform.position.x, player.transform.position.y);
            // Vector2 lookDir = mousePos - new Vector2 (transform.position.x, transform.position.y);
            rotZ = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Euler(0,0,rotZ);
        }
       
        
        
    }

    private void LateUpdate()
    {
        if (gameObject.tag == "EcoGun")
        {
            transform.rotation = ecoAim.GetGunRotZ();
        }
    }
}
