using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HelpButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void HelpClick()
    {
        Debug.Log("WASD to move, right click to take over the camera. When you have power, the E button and left click also may be useful. Destroy the works of the man and get outta there!");
    }
}
