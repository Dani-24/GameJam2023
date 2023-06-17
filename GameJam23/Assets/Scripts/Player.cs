using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float vel = 2;
    private Rigidbody2D playerRB;
    private Vector2 input;
    private Vector2 mousePos;
    private Camera cam;

    void Start()
    {
        playerRB = gameObject.GetComponent<Rigidbody2D>();
       // cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
         input.x = Input.GetAxis("Horizontal");
         input.y = Input.GetAxis("Vertical");

        // mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        
    }

    private void FixedUpdate()
    {
        playerRB.velocity = new Vector2(input.x * vel, input.y * vel);
       
        
        //Vector2 lookDir = mousePos - playerRB.position;

        //float rotZ = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

        //playerRB.rotation = rotZ;
    }
}
