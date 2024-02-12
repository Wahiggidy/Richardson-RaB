using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerRetrieve : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text levelOneTimeText;
    //public TMP_Text levelTwoTimeText;
    public TMP_Text totalTimeText;
    public TMP_Text scoreText;
    void Start()
    {
        if (TimeManager.instance.totalTime != null)
        {
            totalTimeText.text = TimeManager.instance.totalTime;
        }
        if (TimeManager.instance.levelOneTime != null)
        {
            levelOneTimeText.text = TimeManager.instance.levelOneTime;
        }
        /*if (TimeManager.instance.levelTwoTime != null)
        {
            levelTwoTimeText.text = TimeManager.instance.levelTwoTime;
        }
        */
        if (TimeManager.instance.levelTwoTime != null)
        {
            totalTimeText.text = TimeManager.instance.levelOneTime;
        }
        scoreText.text = TimeManager.instance.score.ToString();
           
        
        
        Debug.Log (levelOneTimeText);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
