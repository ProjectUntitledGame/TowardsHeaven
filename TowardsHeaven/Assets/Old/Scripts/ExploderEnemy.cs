using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploderEnemy : MonoBehaviour
{
    public float speed = 3;
    public float explodingSpeed = 1;
    public float stoppingDistance = 2;
    public float fuse = 1f;
    private Transform playerLocation;
    private GameObject globalData;
    private bool beginBoom = false;
    private bool exploding = false;
    private bool inRange = false;
    public CircleCollider2D explosionRad;
    

    void Start()
    {
        playerLocation = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        globalData = GameObject.FindGameObjectWithTag("GlobalData");
    }

    void FixedUpdate()
    {

        if(beginBoom != true)
        {
            if (Vector2.Distance(transform.position, playerLocation.position) > stoppingDistance)
            {

                transform.position = Vector2.MoveTowards(transform.position, playerLocation.position, speed * Time.deltaTime);

            }
            else if (Vector2.Distance(transform.position, playerLocation.position) < stoppingDistance)
            {
                beginBoom = true;
            }
        }
        else
        {
            fuse = fuse - Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, playerLocation.position, explodingSpeed * Time.deltaTime);

            
        }  

    }

    private void Update()
    {
        if(fuse <= 0)
        {
            if(inRange == true)
            {

                
            }

            Destroy(gameObject);
            globalData.GetComponent<GlobalData>().enemiesAlive--;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inRange = false;
        }
    }

}

