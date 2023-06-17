using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletVel = 15f;
    private Transform bulletSpawn;

    void Start()
    {
        bulletSpawn = GameObject.FindGameObjectWithTag("Gun").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            BulletInstance();
        }
    }

    void BulletInstance()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
        bulletRB.AddForce(bulletSpawn.up * bulletVel, ForceMode2D.Impulse);
       // bulletRB.velocity = bulletSpawn.up * bulletVel;
    }

}
