using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletVel = 15f;
    [SerializeField] private Transform bulletSpawn;
    private float  _shootDt = 0f;
    [SerializeField] private float shootDelay = 0.3f;

    private bool isShooting = false;
    private bool ecoCanShoot = false;
    Player playerSc;

    [SerializeField] AudioSource shootAudioClip;
    public float minPitch = 0.9f;
    public float maxPitch = 1.1f;

    void Start()
    {
        //  bulletSpawn = GameObject.FindGameObjectWithTag("Gun").transform;
       if(gameObject.CompareTag("Player")) playerSc = gameObject.GetComponent<Player>();
    }

    void Update()
    {
        _shootDt += Time.deltaTime;
        isShooting = false;
        if (Input.GetKeyDown(KeyCode.Mouse0) && playerSc && !playerSc.isRedo || ecoCanShoot && gameObject.CompareTag("Eco"))
        {
            if (_shootDt >= shootDelay)
            {
                BulletInstance();
                _shootDt = 0;
                isShooting = true;
                ecoCanShoot = false;

                float randomPitch = Random.Range(minPitch, maxPitch);
                shootAudioClip.pitch =randomPitch;

                shootAudioClip.Play();
            }

        }
    }

    public void BulletInstance()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
        bulletRB.AddForce(bulletSpawn.up * bulletVel, ForceMode2D.Impulse);
       // bulletRB.velocity = bulletSpawn.up * bulletVel;
    }

    public bool GetIsShooting()
    {
        return isShooting;
    }
    
    public void SetEcoCanShoot(bool _canShoot=true)
    {
        ecoCanShoot = _canShoot ;
    }



}
