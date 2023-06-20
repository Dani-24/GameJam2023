using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Palanca : MonoBehaviour
{
    InteractuableItem interactiveScrip;

    [SerializeField]
    List<string> InteractTags = new List<string>();

    Animator animator;

    private void Start()
    {
        interactiveScrip = GetComponent<InteractuableItem>();

        animator = GetComponentInChildren<Animator>();

        animator.SetBool("startOn", interactiveScrip.activated);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (var tag in InteractTags)
        {
            if (collision.CompareTag(tag))
            {
                interactiveScrip.activated = !interactiveScrip.activated;
                animator.SetTrigger("toggle");
            }
        }
    }
}
