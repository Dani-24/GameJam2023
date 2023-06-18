using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField]
    int HP = 1;
    [SerializeField]
    float movementSpeed = 1;
    
    public List<string> chaseTheseTags = new List<string>();

    [SerializeField]
    List<string> dieByTheseTags = new List<string>();

    private Vector3 originalScale;
    private Vector3 flippedScale;

    [Header("Movement (Leave PatrolCheckpoint empty if this enemy movementType != patrol)")]
    [SerializeField]
    MovTypes movementType = MovTypes.Wander;

    [SerializeField]
    List<Transform> patrolCheckpoints = new List<Transform>();
    [SerializeField]
    Transform currentCheckpoint;

    [Header("Current Action")]
    [SerializeField]
    EnemyStates currentState = EnemyStates.Idle;

    public GameObject currentlyTargeting;

    Animator animator;
    Rigidbody2D rb;

    [Header("Other VFX")]
    public ParticleSystem DieParticleSystem;

    private void Start()
    {
        flippedScale = originalScale = transform.localScale;
        flippedScale.x *= -1;

        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        if(patrolCheckpoints.Count > 0 && movementType == MovTypes.Patrol)
        {
            currentCheckpoint = patrolCheckpoints[0];
        }
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

        if (currentState == EnemyStates.Chasing && movementType == MovTypes.Wander) 
        {
            MoveToThis(currentlyTargeting.transform);
        }
        else if(movementType == MovTypes.Patrol)
        {
            float dist = Vector2.Distance(transform.position, currentCheckpoint.position);

            // Cambiar Checkpoint
            if(dist < 0.5f)
            {
                for(int i = 0; i < patrolCheckpoints.Count; i++)
                {
                    if (patrolCheckpoints[i] == currentCheckpoint)
                    {
                        if (i != patrolCheckpoints.Count - 1)
                        {
                            currentCheckpoint = patrolCheckpoints[i + 1];
                            break;
                        }
                        else
                        {
                            currentCheckpoint = patrolCheckpoints[0];
                            break;
                        }
                    }
                }
            }
            MoveToThis(currentCheckpoint.transform);
        }

        // Que el enemigo mire a donde se mueve
        if (rb.velocity.x <= 0)
        {
            transform.localScale = originalScale;
        }
        else
        {
            transform.localScale = flippedScale;
        }
    }

    // Movimiento
    void MoveToThis(Transform trans)
    {
        Vector2 dir = (trans.position - transform.position).normalized;

        if (rb.velocity.magnitude < movementSpeed)  // Limite de velocidad
        {
            rb.AddForce(dir * movementSpeed);
        }
    }

    // Collider de muerte ========

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (var tag in dieByTheseTags)
        {
            if (collision.CompareTag(tag))
            {
                HP--;
            }
        }
    }

    // Detection Area

    public void SetTarget(GameObject target)
    {
        currentlyTargeting = target;
    }

    // ESTADOS DEL BICHO =========

    public enum EnemyStates
    {
        Idle,
        Chasing,
        Attacking,
        Dead
    }

    public enum MovTypes
    {
        Wander,
        Patrol,
        Stationary
    }
}
