using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {


    public int health;
    public int noOfNotes; 

    public Image[] notes;
    public Sprite fullNote;
    public Sprite emptyNote; 



	public void OnClickQuitButton()
    {
        print("Quit button was clicked");
        Application.Quit();
    }

    private void Update()
    {
        
        if (health>noOfNotes) 
        {
            health = noOfNotes;
        }
        
        for (int i = 0; i < notes.Length; i++) 
        {
            if (i<health)
            {
                notes[i].sprite = fullNote;
            }
            else
            {
                notes[i].sprite = emptyNote;
            }

            if(i<noOfNotes)
            {
                notes[i].enabled = true;
            }
            else
            {
                notes[i].enabled = false;
            }



        }








    }



}
