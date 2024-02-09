using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static TimeManager instance;
    public string levelOneTime; 
    public string levelTwoTime;
    public float levelOneMin;
    public float levelTwoMin;
    public float levelOneSec;
    public float levelTwoSec;
    public string totalTime; 

    void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        totalTime = string.Format("{0:D2}:{1:D2}", levelOneMin + levelTwoMin, levelTwoSec + levelOneSec);
    }


    }
