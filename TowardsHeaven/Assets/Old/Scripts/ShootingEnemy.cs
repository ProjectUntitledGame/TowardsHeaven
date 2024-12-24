using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    public GameObject projectile;
    private GameObject globalData;
    private Transform playerLocation;
    public float stoppingDistance;
    public float retreatDistance;
    public float shotTimer;
    public float shotTimerLimit;
    public float speed;
    int animState = 1;
    Animator anim;
    int currentState = 1;
    
    void Start()
    {
        playerLocation = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        shotTimer = shotTimerLimit;
        anim = GetComponent<Animator>();
        globalData = GameObject.FindGameObjectWithTag("GlobalData");
    }

    private void Update()
    {
        
        if(shotTimer <= 0)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            shotTimer = shotTimerLimit;
           
        }
        else
        {
            shotTimer -= Time.deltaTime;
        }
        
        if(animState != currentState)
        {
            animState = currentState;
            anim.SetInteger("animState", animState);
        }
    }

    void FixedUpdate()
    {


        if (Vector2.Distance(transform.position, playerLocation.position) > stoppingDistance)
        {

            transform.position = Vector2.MoveTowards(transform.position, playerLocation.position, speed * Time.deltaTime);
            currentState = 1;

        }
        else if (Vector2.Distance(transform.position, playerLocation.position) < stoppingDistance && Vector2.Distance(transform.position, playerLocation.position) > retreatDistance)
        {
            transform.position = this.transform.position;
            currentState = 0;
        }
        else if(Vector2.Distance(transform.position, playerLocation.position) < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerLocation.position, -speed * Time.deltaTime);
            currentState = 1;
        }

    }

    


}
