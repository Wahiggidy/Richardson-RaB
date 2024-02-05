using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public int sceneIndex;
    public Animation transition;
    void Start()
    {
        //transition = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadNextLevel()
    {
        
        StartCoroutine(LoadLevel(sceneIndex));
    }

    IEnumerator LoadLevel(int index)
    {
        transition.Play("Crossfade -- start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneIndex);
    }





}
