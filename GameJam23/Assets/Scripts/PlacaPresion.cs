using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacaPresion : MonoBehaviour
{
    public bool activated;

    [SerializeField]
    List<string> InteractTags = new List<string>();

    Animator animator;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (var tag in InteractTags)
        {
            if (collision.CompareTag(tag))
            {
                activated = true;
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
                activated = false;
                animator.SetTrigger("trigger");
            }
        }
    }
}
