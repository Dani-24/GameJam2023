using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Palanca : MonoBehaviour
{
    public bool activated;

    [SerializeField]
    List<string> InteractTags = new List<string>();

    Animator animator;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();

        animator.SetBool("startOn", activated);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (var tag in InteractTags)
        {
            if (collision.CompareTag(tag))
            {
                activated = !activated;
                animator.SetTrigger("toggle");
            }
        }
    }
}
