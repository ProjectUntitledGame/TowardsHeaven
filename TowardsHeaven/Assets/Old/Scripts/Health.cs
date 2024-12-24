using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int healthvalue;
    private int killCount;
    private GameObject globalData;
    public GameObject gameOverScreen;
    private string objectTag;

    private void Start()
    {
        objectTag = this.tag;
        globalData = GameObject.FindGameObjectWithTag("GlobalData");
    }

    // Update is called once per frame
    void Update()
    {
       
       if (healthvalue <= 0)
        {
            if (objectTag == "Player")
            {
                gameOverScreen.SetActive(true);
            }
            if (objectTag == "Enemy" || objectTag == "Boss")
            {
                Destroy(gameObject);
                globalData.GetComponent<Saving>().GetKill();
                globalData.GetComponent<GlobalData>().enemiesAlive--;

            }
        }   
    }


    
}
