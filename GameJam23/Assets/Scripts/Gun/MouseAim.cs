using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAim : MonoBehaviour
{
    public Vector2 mousePos;
    private Camera cam;
    private GameObject player;
    private Rigidbody2D playerRB;

    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerRB = player.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {


        Vector2 lookDir = mousePos - playerRB.position;//new Vector2 (playerRB.position.x, playerRB.position.y);



       // Vector2 lookDir = mousePos - new Vector2 (player.transform.position.x, player.transform.position.y);
       // Vector2 lookDir = mousePos - new Vector2 (transform.position.x, transform.position.y);
        float rotZ = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg  -90f;
        
        
        transform.rotation = Quaternion.Euler(0,0,rotZ);


    }
}
