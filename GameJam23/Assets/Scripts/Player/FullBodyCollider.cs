using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullBodyCollider : MonoBehaviour
{
    // --- Detectar Collisiones q pegan al player ---

    Player playerScript;

    private void Start()
    {
        playerScript = GetComponentInParent<Player>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (var tag in playerScript.dieByTheseTags)
        {
            if (collision.gameObject.tag == tag)
            {
                playerScript.TakeDamage(1);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (var tag in playerScript.dieByTheseTags)
        {
            if (collision.tag == tag)
            {
                playerScript.TakeDamage(1);
            }
        }
    }
}
