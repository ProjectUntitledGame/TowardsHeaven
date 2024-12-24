using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Saving : MonoBehaviour
{
    public GameObject GlobalData;
    public Text score;
    public Text highScore;
    public Text totalKills;
    private int killcount = 0;
    private int highKillCount;
    private int totalKillsNum;
    

    private void Start()
    {
        
        highKillCount = PlayerPrefs.GetInt("Highscore", 0);
        killcount = PlayerPrefs.GetInt("Score", 0);
        totalKillsNum = PlayerPrefs.GetInt("TotalKills", 0);
    }

    private void Update()
    {
        
        score.text = ("Killcount: " + killcount.ToString());
        highScore.text = ("Highest Killcount: " + highKillCount.ToString());
        totalKills.text = ("Total Kills: " + totalKillsNum.ToString());
        
        if(killcount > highKillCount)
        {
            highKillCount = killcount;
        }

    }

    public void ClickSave()
    {
        if(killcount < highKillCount)
        {
            Debug.Log("Are you sure");
        }
        else
        {
            PlayerPrefs.SetInt("Highscore", killcount);
        }
        
    }

    public void ResetScore()
    {
    }  

    public void GetKill()
    {
        killcount ++;
        totalKillsNum++;
        PlayerPrefs.SetInt("TotalKills", totalKillsNum);
    }

    
    public void ResetStats()
    {
        PlayerPrefs.DeleteKey("Highscore");
        PlayerPrefs.DeleteKey("Levels");
        PlayerPrefs.DeleteKey("Score");
        PlayerPrefs.DeleteKey("TotalLevels");
        PlayerPrefs.DeleteKey("TotalKills");
        PlayerPrefs.DeleteKey("LevelHigh");
        
    }

    
}
