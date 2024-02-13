using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject pauseMenu; 
    



    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume() 
    { 
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }    

    public void Home(int SceneID)
    {
        Time.timeScale = 1f;
        DestroyImmediate(BGMusic.instance.gameObject);
        SceneManager.LoadScene(0);
    }


}
