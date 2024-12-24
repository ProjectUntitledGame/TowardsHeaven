using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private GameObject globalData;
    public GameObject[] door;

    void Start()
    {
        globalData = GameObject.FindGameObjectWithTag("GlobalData");
    }

    // Update is called once per frame
    void Update()
    {
        if(globalData.GetComponent<GlobalData>().enemiesAlive > 0)
        {
            for(int i = 0; i<door.Length; i++)
            {
                door[i].SetActive(true);
            }

        }
        else
        {
            for (int i = 0; i < door.Length; i++)
            {
                door[i].SetActive(false);
            }
            
        }
    }
}
