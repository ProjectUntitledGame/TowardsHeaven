using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersProjectile : MonoBehaviour
{
    private Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject player;
    public float speed = 20f;


    private void OnCollisionEnter2D(Collision2D other)
    {
        DestroySelf();
        if (other.gameObject.CompareTag("Enemy"))
        {
            DestroySelf();
            other.gameObject.GetComponent<Health>().healthvalue--;

        }

        
    }



    
    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
