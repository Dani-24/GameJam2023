using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    [SerializeField]
    int HP = 1;
    [SerializeField]
    float movementSpeed = 1;

    [SerializeField]
    EnemyStates currentState = EnemyStates.Idle;

    [SerializeField]
    GameObject currentlyTargeting;

    Animator animator;
    Rigidbody2D rb;

    public ParticleSystem DieParticleSystem;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (HP <= 0)
        {
            currentState = EnemyStates.Dead;
        }
        else if (currentlyTargeting != null)
        {
            currentState = EnemyStates.Chasing;
            animator.SetBool("idle", false);
        }
        else { 
            currentState = EnemyStates.Idle;
            animator.SetBool("idle", true);
        }

        if (currentState == EnemyStates.Dead) { 
            DieParticleSystem.Play();
            animator.SetTrigger("die");
            Destroy(this.gameObject,1);
        }
    }

    private void FixedUpdate()
    {
        // Movimiento Enemigo ===

        if (currentState == EnemyStates.Chasing) 
        {
            Vector2 dir = (currentlyTargeting.transform.position - transform.position).normalized;

            if (rb.velocity.magnitude < movementSpeed)  // Limite de velocidad
            {
                rb.AddForce(dir * movementSpeed);
            }
        }
    }

    // AREA DE DETECCIÓN =========

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            currentlyTargeting = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            currentlyTargeting = null;
        }
    }

    // COLLIDER DE MUERTE =========

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            HP--;
        }
    }

    // ESTADOS DEL BICHO =========

    public enum EnemyStates
    {
        Idle,
        Chasing,
        Attacking,
        Dead
    }
}
