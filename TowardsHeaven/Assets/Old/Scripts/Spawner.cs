using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject globalData;
    private int enemyNum = 0;
    private Vector2 SpawnLocation;
    private int enemyTotal;
    
    void Start()
    {
        enemyTotal = UnityEngine.Random.Range(1, 7);
        globalData = GameObject.FindGameObjectWithTag("GlobalData");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            while (enemyTotal > 0)
            {
                NewSpawn();
                enemyTotal--;
            }
        }
        
       
    }

    public void NewSpawn()
    {
        SpawnLocation.x = transform.position.x - UnityEngine.Random.Range(9, -9);
        SpawnLocation.y = transform.position.y - UnityEngine.Random.Range(4, -4);
        enemyNum = UnityEngine.Random.Range(0, 3);
        Instantiate(enemies[enemyNum], SpawnLocation, transform.rotation);
        globalData.GetComponent<GlobalData>().enemiesAlive++;
    }

    // Update is called once per frame
    private void Update()
    {
        if(enemyTotal <= 0)
        {
           // DestroySelf();
        }
    }

    /*private void DestroySelf()
    {
        Destroy(gameObject);
    }*/


}
