using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;

    private Transform player;
    private Vector2 target;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
    }

    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if(transform.position.x == target.x && transform.position.y == target.y)
        {
            DestroySelf();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy")||other.CompareTag("Boss"))
        {

        }
        else
        {
            if (other.CompareTag("Player"))
            {
                DestroySelf();
                other.GetComponent<Health>().healthvalue--;
            }
            DestroySelf();
        }
        
       
        
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
