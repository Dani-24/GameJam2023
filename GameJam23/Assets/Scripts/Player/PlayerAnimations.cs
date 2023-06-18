using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] Animator playerAnimator;
    private Vector2 input;
    
    [SerializeField] private MouseAim mouseAim;
    private Vector2 mousePos;

    private bool isAimingRight;

    void Update()
    {
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

        mousePos = mouseAim.mousePos;

        if (mousePos.x > transform.position.x)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), Mathf.Abs(transform.localScale.y), Mathf.Abs(transform.localScale.z));
            isAimingRight = true;
        }
        else if (mousePos.x < transform.position.x)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), Mathf.Abs(transform.localScale.y), Mathf.Abs(transform.localScale.z));
            isAimingRight = false;
        }

        if (input.magnitude > 0)
        {
            if (playerAnimator.GetBool("IsWalking") == false) 
            {
                playerAnimator.SetBool("IsWalking", true);
            }

            if (input.x < 0)
            {
                if (isAimingRight)
                {
                    if (playerAnimator.GetBool("IsFront") == true)
                    {
                        playerAnimator.SetBool("IsFront", false);
                    }
                }
                else 
                {
                    if (playerAnimator.GetBool("IsFront") == false)
                    {
                        playerAnimator.SetBool("IsFront", true);
                    }
                }
            }
            else if (input.x > 0)
            {
                if (isAimingRight)
                {
                    if (playerAnimator.GetBool("IsFront") == false)
                    {
                        playerAnimator.SetBool("IsFront", true);
                    }
                }
                else
                {
                    if (playerAnimator.GetBool("IsFront") == true)
                    {
                        playerAnimator.SetBool("IsFront", false);
                    }
                }
            }
        }
        else if (playerAnimator.GetBool("IsWalking") == true && input.magnitude == 0)
        {
            playerAnimator.SetBool("IsWalking", false);
        }
        
    }
}
