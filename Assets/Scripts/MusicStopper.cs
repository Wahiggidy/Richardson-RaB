using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicStopper : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource musicSource; 
    public AudioClip lossTheme;
    public float delayMusic; 
    void Start()
    {
        musicSource = BGMusic.instance.GetComponent<AudioSource>();
        if (SceneManager.GetActiveScene().name == "LOSE" || SceneManager.GetActiveScene().name == "LOSE 2")
            BGMusic.instance.GetComponent<AudioSource>().Pause();
        Invoke("StartLoseTheme", delayMusic);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartLoseTheme()
    {
        musicSource.clip = lossTheme;
        BGMusic.instance.GetComponent<AudioSource>().Play();
    }
}
