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

    private float bpm = 30f;
    private float timePerBeat;
    private float timer;




    void Start () 
    {
        timePerBeat = 60f / bpm;
    }



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



            // For movement
            timer += Time.deltaTime;

            float sineValue = Mathf.Sin(2 * Mathf.PI * timer / timePerBeat) * 0.5f + 0.5f;


            for (int a = 0; a < notes.Length; a++)
            {
                if (notes[a].sprite == fullNote)
                {
                    Vector3 newPosition = notes[a].rectTransform.localPosition;
                    newPosition.y += Mathf.Lerp(-.8f, .8f, sineValue); // Adjust the range as needed
                    notes[a].rectTransform.localPosition = newPosition;
                } 
                
            }

            if (timer > timePerBeat)
            {
                timer -= timePerBeat;
            }



        }








    }



}
