using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Enemy1 : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField]
    int HP = 1;
    int fullHP;

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
    public Rigidbody2D rb;

    [Header("Required Prefabs")]
    [SerializeField]
    GameObject ShieldGameObject;

    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    Transform DetectionAreaTransform;

    [SerializeField] private AudioSource shootAudioClip;

    public float minPitch = 0.1f;
    public float maxPitch = 0.6f;

    [Header("Other Bullshit")]
    public ParticleSystem DieParticleSystem;

    [SerializeField]
    GameObject shootRTop;
    [SerializeField]
    GameObject shootRBot;
    [SerializeField]
    GameObject shootLTop;
    [SerializeField]
    GameObject shootLBot;

    Player playerScript;

    [SerializeField] BoxCollider2D boxCollider;

    private void Start()
    {
        fullHP = HP;

        flippedScale = originalScale = transform.localScale;
        flippedScale.x *= -1;

        if (!startLookingLeft)
        {
            transform.localScale = flippedScale;
        }

        actualShootingCooldown = shootingCooldown;

        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        if (patrolCheckpoints.Count > 0 && movementType == MovTypes.Patrol)
        {
            currentCheckpoint = patrolCheckpoints[0];
        }

        ShieldPlacer();
    }

    void ShieldPlacer()
    {
        if (shieldDirection != ShieldDirection.Disabled)
        {
            ShieldGameObject.SetActive(true);

            switch (shieldDirection)
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
                case ShieldDirection.Aiming:
                    break;
            }
        }
    }

    void Update()
    {
        if (!playerScript.isRedo)
        {
            if (HP <= 0)
            {
                currentState = EnemyStates.Dead;

                DieParticleSystem.Play();
                animator.SetTrigger("die");
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

                if (shieldDirection == ShieldDirection.Aiming)
                {
                    Vector2 lookDir = currentlyTargeting.transform.position - transform.position;
                    float rotZ = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 180f;
                    ShieldGameObject.transform.rotation = Quaternion.Euler(0, 0, rotZ);
                }
            }
            else
            {
                currentState = EnemyStates.Idle;
                animator.SetBool("idle", true);
            }
        }
        else
        {
            DeactivateCollider();
        }
    }

    private void FixedUpdate()
    {
        if (!playerScript.isRedo && currentState != EnemyStates.Dead)
        {
            // Movimiento Enemigo ===

            if (currentState == EnemyStates.Chasing && movementType == MovTypes.Wander)
            {
                MoveToThis(currentlyTargeting.transform);
            }
            else if (movementType == MovTypes.Patrol)
            {
                if (currentState == EnemyStates.Idle)
                {
                    float dist = Vector2.Distance(transform.position, currentCheckpoint.position);

                    // Cambiar Checkpoint
                    if (dist < 0.5f)
                    {
                        for (int i = 0; i < patrolCheckpoints.Count; i++)
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
                else
                {
                    if (currentlyTargeting != null) MoveToThis(currentlyTargeting.transform);
                }
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

            if (movementType == MovTypes.Stationary && currentlyTargeting != null)
            {
                if ((currentlyTargeting.transform.position.x < transform.position.x))
                {
                    transform.localScale = originalScale;
                }
                else
                {
                    transform.localScale = flippedScale;
                }
            }

            // Update Posici�n del Shield y del area de detecci�n
            ShieldGameObject.transform.position = transform.position;
            DetectionAreaTransform.position = transform.position;
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

            float randomPitch = UnityEngine.Random.Range(minPitch, maxPitch);

            shootAudioClip.pitch = randomPitch;

            shootAudioClip.Play();
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

    public void ActivateEnemy()
    {
        currentState = EnemyStates.Idle;
        HP = fullHP;
        animator.ResetTrigger("die");
        animator.Play("Idle");

        ActivateCollider();

        ShieldPlacer();

        if (!startLookingLeft)
        {
            transform.localScale = flippedScale;
        }
        else
        {
            transform.localScale = originalScale;
        }

        rb.WakeUp();
    }

    public void DeactivateEnemy()
    {
        this.transform.parent.gameObject.SetActive(false);
    }

    void ActivateCollider()
    {
        boxCollider.enabled = true;
    }

    public void DeactivateCollider()
    {
        boxCollider.enabled = false;
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
        Aiming,
        Disabled
    }
}
