using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Palanca : MonoBehaviour
{
    InteractuableItem interactiveScript;

    [SerializeField]
    List<string> InteractTags = new List<string>();

    Animator animator;

    private void Start()
    {
        interactiveScript = GetComponent<InteractuableItem>();

        animator = GetComponentInChildren<Animator>();

        animator.SetBool("startOn", interactiveScript.activated);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (var tag in InteractTags)
        {
            if (collision.CompareTag(tag))
            {
                interactiveScript.activated = !interactiveScript.activated;
                animator.SetTrigger("toggle");
            }
        }
    }

    public void ResetAnimation()
    {
        animator.SetTrigger("toggle");
    }
}
