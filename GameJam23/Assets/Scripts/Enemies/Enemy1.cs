using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor.U2D;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField]
    int HP = 1;
    [SerializeField]
    float movementSpeed = 1;

    [SerializeField]
    float shootingCooldown = 1.0f;
    float actualShootingCooldown;

    [SerializeField]
    float bulletVel = 1.0f;

    [SerializeField]
    bool startLookingLeft = true;

    public List<string> chaseTheseTags = new List<string>();

    [SerializeField]
    List<string> dieByTheseTags = new List<string>();

    [SerializeField]
    ShieldDirection shieldDirection = ShieldDirection.Disabled;

    private Vector3 originalScale;
    private Vector3 flippedScale;

    [SerializeField]
    Transform bulletSpawnPosition;

    [Header("Movement")]
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

    [Header("Required Prefabs")]
    [SerializeField]
    GameObject ShieldGameObject;

    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    Transform DetectionAreaTransform;

    [Header("Other")]
    public ParticleSystem DieParticleSystem;

    [SerializeField]
    GameObject shootRTop;
    [SerializeField]
    GameObject shootRBot;
    [SerializeField]
    GameObject shootLTop;
    [SerializeField]
    GameObject shootLBot;

    private void Start()
    {
        

        flippedScale = originalScale = transform.localScale;
        flippedScale.x *= -1;

        if (!startLookingLeft)
        {
            transform.localScale = flippedScale;
        }

        actualShootingCooldown = shootingCooldown;

        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        if(patrolCheckpoints.Count > 0 && movementType == MovTypes.Patrol)
        {
            currentCheckpoint = patrolCheckpoints[0];
        }

        if(shieldDirection != ShieldDirection.Disabled)
        {
            ShieldGameObject.SetActive(true);

            switch(shieldDirection)
            {
                case ShieldDirection.Down:
                    ShieldGameObject.transform.rotation = Quaternion.Euler(0, 0, 90);
                    break;
                case ShieldDirection.Up:
                    ShieldGameObject.transform.rotation = Quaternion.Euler(0, 0, -90);
                    break;
                case ShieldDirection.Left:
                    ShieldGameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                    break;
                case ShieldDirection.Right:
                    ShieldGameObject.transform.rotation = Quaternion.Euler(0, 0, 180);
                    break;
            }
        }
    }

    void Update()
    {
        if (HP <= 0)
        {
            currentState = EnemyStates.Dead;

            DieParticleSystem.Play();
            animator.SetTrigger("die");
            Destroy(this.transform.parent.gameObject, 1);
        }
        else if (currentlyTargeting != null)
        {
            if (actualShootingCooldown > 0)
            {
                currentState = EnemyStates.Chasing;
                animator.SetBool("idle", false);

                actualShootingCooldown -= Time.deltaTime;
            }
            else
            {
                EnemyShoot();
            }
        }
        else { 
            currentState = EnemyStates.Idle;
            animator.SetBool("idle", true);
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
        if (rb.velocity.x != 0 && movementType != MovTypes.Fixed)
        {
            if (rb.velocity.x < 0)
            {
                transform.localScale = originalScale;
            }
            else
            {
                transform.localScale = flippedScale;
            }
        }

        // Update Posición del Shield y del area de detección
        ShieldGameObject.transform.position = transform.position;
        DetectionAreaTransform.position = transform.position;
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

    // Pium Pium por parte del enemy al player

    void EnemyShoot()
    {
        if (CalculateDirection())
        {
            currentState = EnemyStates.Attacking;

            animator.SetTrigger("shoot");

            actualShootingCooldown = shootingCooldown;

            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPosition.position, bulletSpawnPosition.rotation);
            Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
            bulletRB.AddForce(bulletSpawnPosition.up * bulletVel, ForceMode2D.Impulse);
        }
    }

    bool CalculateDirection()
    {
        Vector2 lookDir = currentlyTargeting.transform.position - transform.position;
        float rotZ = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        bulletSpawnPosition.rotation = Quaternion.Euler(0, 0, rotZ);

        if (movementType == MovTypes.Fixed)
        {
            bool isRight;

            if (rotZ < 0 && rotZ > -180)
            {
                isRight = true;
            }
            else
            {
                isRight = false;
            }

            if (isRight && transform.localScale == flippedScale || !isRight && transform.localScale == originalScale)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        return true;
    }

    // ESTADOS DEL BICHO ========

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
        Stationary,
        Fixed
    }

    public enum ShieldDirection
    {
        Left,
        Right,
        Up,
        Down,
        Disabled
    }
}
