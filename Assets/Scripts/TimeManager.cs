using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TimeManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static TimeManager instance;
    public  string levelOneTime; 
    public  string levelTwoTime;
    public int score;
    public  int levelOneMin;
    public  int levelTwoMin;
    public  int levelOneSec;
    public  int levelTwoSec;
    public  string totalTime;
    public bool readyForTotal;

    void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        
        
            
        
        
    }
    public void GetTotal()
    {
        //totalTime = string.Format("{0:D2}:{1:D2}", levelOneMin + levelTwoMin, levelOneSec + levelTwoSec);   // This info is gotten from player script on level finish

        int totalMinutes = (int)levelOneMin + (int)levelTwoMin;
        int totalSeconds = (int)levelOneSec + (int)levelTwoSec;

        // Handle excess seconds
        if (totalSeconds >= 60)
        {
            totalMinutes += totalSeconds / 60;
            totalSeconds %= 60;
        }

        totalTime = string.Format("{0:D2}:{1:D2}", totalMinutes, totalSeconds);
    }

    }
