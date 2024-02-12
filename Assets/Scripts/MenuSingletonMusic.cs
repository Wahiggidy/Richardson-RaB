using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSingletonMusic : MonoBehaviour
{
    // Start is called before the first frame update
    public static MenuSingletonMusic instance;

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

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            Destroy(gameObject);
        }
    }

}
