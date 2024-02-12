using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerRetrieve : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text levelOneTimeText;
    public TMP_Text levelTwoTimeText;
    public TMP_Text totalTimeText;
    void Start()
    {
        if (TimeManager.instance.levelOneTime != null)
        {
            levelOneTimeText = TimeManager.instance.levelOneTime;
        }
        if (TimeManager.instance.levelTwoTime != null)
        {
            levelTwoTimeText = TimeManager.instance.levelTwoTime;
        }
        if (TimeManager.instance.totalTime != null)
        {
            totalTimeText = TimeManager.instance.totalTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
