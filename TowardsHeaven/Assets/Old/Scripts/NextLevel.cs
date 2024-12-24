using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NextLevel : MonoBehaviour
{
    private GameObject globalData;

    private void Start()
    {
        globalData = GameObject.FindGameObjectWithTag("GlobalData");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //globalData.GetComponent<Saving>().BeatLevel();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
    }


}
