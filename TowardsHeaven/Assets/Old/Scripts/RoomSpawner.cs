using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    //1 = down
    //2  = up
    //3 = left
    //4 = right
    private RoomTemplates templates;
    private int random;
    private bool spawned = false;
    public float waitTime = 4f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, waitTime);
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.75f);
    }

    // Update is called once per frame
    void Spawn()
    {
        if(spawned == false)
        {
            if (openingDirection == 1)
            {
                random = Random.Range(0, templates.bottomRooms.Length);
                Instantiate(templates.bottomRooms[random], transform.position, templates.bottomRooms[random].transform.rotation);
                
            }
            else if (openingDirection == 2)
            {
                random = Random.Range(0, templates.topRooms.Length);
                Instantiate(templates.topRooms[random], transform.position, templates.topRooms[random].transform.rotation);

            }
            else if (openingDirection == 3)
            {
                random = Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[random], transform.position, templates.leftRooms[random].transform.rotation);

            }
            else if (openingDirection == 4)
            {
                random = Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[random], transform.position, templates.rightRooms[random].transform.rotation);

            }
            spawned = true;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("RoomSpawn")) 
        {
            
            try
            {
                if (!other.GetComponent<RoomSpawner>().spawned && !spawned)
                {
                    Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                    
                }
            }
            catch (System.Exception)
            {
                
                Destroy(gameObject);

            }
            spawned = true;
            
        }


    }
}
