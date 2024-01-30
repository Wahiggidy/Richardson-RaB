using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BGMusic : MonoBehaviour
{
    public static BGMusic instance;

    void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        if (!instance.GetComponent<AudioSource>().isPlaying)
        {
            instance.GetComponent<AudioSource>().Play();
        }
    }
        


}