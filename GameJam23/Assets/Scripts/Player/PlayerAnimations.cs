using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] Animator playerAnimator;
    [SerializeField] Rigidbody2D playerBody;
    private float velocity;
    [SerializeField] float minVelocity;
    private Vector2 input;

    void Update()
    {
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

        if (input.magnitude > 0)
        {
            if (playerAnimator.GetBool("IsWalking") == false) 
            {
                playerAnimator.SetBool("IsWalking", true);
            }
            
            if (input.x < 0 && playerAnimator.GetBool("IsRight") == true)
            {
                playerAnimator.SetBool("IsRight", false);
            } 
            else if (input.x > 0 && playerAnimator.GetBool("IsRight") == false)
            {
                playerAnimator.SetBool("IsRight", true);
            }
        }
        else if (playerAnimator.GetBool("IsWalking") == true && input.magnitude == 0)
        {
            playerAnimator.SetBool("IsWalking", false);
        }
        
    }

    private void FixedUpdate()
    {
        //velocity = playerBody.velocity.magnitude;
        //if (playerAnimator.GetBool("IsWalking") == false && velocity > minVelocity)
        //{
        //    playerAnimator.SetBool("IsWalking", true);
        //}
        //else if (playerAnimator.GetBool("IsWalking") == true && velocity <= minVelocity)
        //{
        //    playerAnimator.SetBool("IsWalking", false);
        //}
    }

}
