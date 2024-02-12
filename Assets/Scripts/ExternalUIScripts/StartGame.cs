using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    // Start is called before the first frame update
    public int gameStartScene;
    private AudioSource musicSource;
    public AudioClip musicClip;
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (BGMusic.instance != null )
        {
            musicSource = BGMusic.instance.GetComponent<AudioSource>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel()
    {
       if (musicClip != null)
        {
            musicSource.clip = musicClip;
        }
        else
        {
            if (musicSource != null) 
            {
                BGMusic.instance.GetComponent<AudioSource>().Pause();
            }
            
        }
        SceneManager.LoadScene(gameStartScene);
    }
}
