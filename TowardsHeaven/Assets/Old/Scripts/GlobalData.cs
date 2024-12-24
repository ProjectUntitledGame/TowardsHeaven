using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

public class GlobalData : MonoBehaviour
{
    public GameObject globalData;
    public GameObject PauseMenu;
    public GameObject Quit;
    private bool paused = false;
    private bool Checking = false;
    public bool fight = false;
    public int level = 1;
    public int enemiesAlive;
    public List<int> roomsSpawns;
    

    

    void Start()
    {
        
        PauseMenu.SetActive(false);
        Quit.SetActive(false);
        
    }


    public void Paused()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
    }
    public void Unpause()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if(paused & Checking)
            {
                Quit.SetActive(false);
                Checking = false;
                PauseMenu.SetActive(true);
                
            }
            else if (!paused)
            {
                Paused();
            }
            else
            {
                Unpause();
            }
        }


    }

    public void NewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }    
    

    public void QuitPressed()
    {
        Quit.SetActive(true);
        Checking = true;
        PauseMenu.SetActive(false);
    }

    public void AbortQuit()
    {
        Quit.SetActive(false);
        Checking = false;
        Paused();
    }

    public void ToMenu()
    {
        SceneManager.LoadScene(0);
        Unpause();
    }

    public void ToDesktop()
    {
        Application.Quit();
        Unpause();
    }

    public void spawns()
    {
        PlayerPrefs.SetInt("myList_count", roomsSpawns.Count);

        for (int i = 0; i < roomsSpawns.Count; i++)
            PlayerPrefs.SetInt("myList_" + i, roomsSpawns[i]);
    }
}
