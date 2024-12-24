using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;
    public float delay = 0.25f;
    public float delayMax = 0.25f;
    // Update is called once per frame

    private void Start()
    {
        delay = 0;
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
            
        }
        if(delay > 0)
        {
            delay -= Time.deltaTime;
        }
    }
    void Shoot()
    {
        if(delay <= 0)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
            delay = delayMax;
        }
        
    }
}
