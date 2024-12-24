using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BossShooter : MonoBehaviour
{
    [SerializeField]
    int projectileCount;
    [SerializeField]
    GameObject projectile;
    Vector2 startPoint;
    private float radius;
    private float movespeed;
    public float shotTimer;
    public float shotTimerLimit;
    public float speed;
    public float stoppingDistance;
    public float retreatDistance;
    public float rotSpeed = 1;
    private Transform playerLocation;

    void Start()
    {
        playerLocation = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        radius = 5f;
        movespeed = 5f;
        shotTimer = shotTimerLimit;
    }

    
    void Update()
    {

        startPoint = transform.position;

        if (shotTimer <= 0)
        {
            SpawnProjectiles(projectileCount);
            shotTimer = shotTimerLimit;
        }
        else
        {
            shotTimer -= Time.deltaTime;
        }
        
        

    }

    void FixedUpdate()
    {


        if (Vector2.Distance(transform.position, playerLocation.position) > stoppingDistance)
        {

            transform.position = Vector2.MoveTowards(transform.position, playerLocation.position, speed * Time.deltaTime);
            

        }
        else if (Vector2.Distance(transform.position, playerLocation.position) < stoppingDistance && Vector2.Distance(transform.position, playerLocation.position) > retreatDistance)
        {
            transform.position = this.transform.position;
            
        }
        else if (Vector2.Distance(transform.position, playerLocation.position) < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerLocation.position, -speed * Time.deltaTime);
            
        }

    }


    void SpawnProjectiles(int projectileCount)
    {
        float angleStep = 360f / projectileCount;
        float angle = 0f;

        for(int i = 0; i <= projectileCount -1; i++)
        {
            float projectileDirXPos = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float projectileDirYPos = startPoint.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            Vector2 projectileVector = new Vector2(projectileDirXPos, projectileDirYPos);
            Vector2 projectileMoveDir = (projectileVector - startPoint).normalized * movespeed;

            var projectileInstantiate = Instantiate(projectile, startPoint, Quaternion.identity);
            projectileInstantiate.GetComponent<Rigidbody2D>().velocity =
                new Vector2(projectileMoveDir.x, projectileMoveDir.y);
            angle += angleStep;
        }

    }
}
