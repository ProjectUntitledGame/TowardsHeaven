using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    private void Start()
    {
        PlayerPrefs.DeleteKey("Score");
        PlayerPrefs.DeleteKey("Levels");
    }
    public void PlayButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ExitButton()
    {
        Application.Quit();
    }
}
