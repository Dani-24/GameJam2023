using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacaPresion : MonoBehaviour
{
    InteractuableItem interactiveScrip;

    [SerializeField]
    List<string> InteractTags = new List<string>();

    Animator animator;

    private void Start()
    {
        interactiveScrip = GetComponent<InteractuableItem>();
        animator = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (var tag in InteractTags)
        {
            if (collision.CompareTag(tag))
            {
                interactiveScrip.activated = true;
                animator.SetTrigger("trigger");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        foreach (var tag in InteractTags)
        {
            if (collision.CompareTag(tag))
            {
                interactiveScrip.activated = false;
                animator.SetTrigger("trigger");
            }
        }
    }
}
