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
    public  float levelOneMin;
    public  float levelTwoMin;
    public  float levelOneSec;
    public  float levelTwoSec;
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
        totalTime = string.Format("{0:D2}:{1:D2}", levelOneMin + levelTwoMin, levelTwoSec + levelOneSec);   // This info is gotten from player script on level finish
    }

    }
