using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacaPresion : MonoBehaviour
{
    InteractuableItem interactiveScript;

    [SerializeField]
    List<string> InteractTags = new List<string>();

    Animator animator;
    [SerializeField] private AudioSource audioClip;
    void Start()
    {
        interactiveScript = GetComponent<InteractuableItem>();
        animator = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (var tag in InteractTags)
        {
            if (collision.CompareTag(tag))
            {
                interactiveScript.activated = true;
                animator.SetTrigger("trigger");
                audioClip.Play();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        foreach (var tag in InteractTags)
        {
            if (collision.CompareTag(tag))
            {
                interactiveScript.activated = false;
                animator.SetTrigger("trigger");
            }
        }
    }
}
