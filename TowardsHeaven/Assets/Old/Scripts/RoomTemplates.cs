using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;
    public GameObject closedRoom;
    public GameObject loadingScreen;
    public List<GameObject> rooms;
    public float waitTime;
    private bool spawnedBoss;
    public GameObject boss;
    public GameObject camera;
    private Vector2 spawnLocation;

    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        
        if (waitTime <= 0 && spawnedBoss == false)
        {  
            for (int i = 0; i < rooms.Count; i++)
            {
                if(i == rooms.Count - 1)
                {
                    
                    Instantiate(boss, rooms[i].transform.position, Quaternion.identity);
                    spawnedBoss = true;
                    loadingScreen.SetActive(false);
                }
            }
        }
        else
        {
            if(waitTime > 0)
            {
                waitTime -= Time.deltaTime;
            }
            
        }
    }
}
