using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public bool basicBackandForth;
    public bool square;
    public bool circular;
    public bool horizontal;
    public bool manual;              // Uses a separate script for movement, lookat

    public float panSpeed;                 // Y, can be any speed basically
    public float pauseDuration;
    public float panDuration;
    private float pauseTime;

    private bool forward; 

    // Above are global or for basic version, below are advanced variables
    public float panSpeedVertical;          // Z, needs to be slower            (without deformation -- with deformation, may be able to be higher)
    public float panSpeedHorizontal;        // X, needs to be mid speed 
    public int selection;
    public float smoothRotationSpeed = 2f;

    public float circleRadius = 5f;
    public Transform target;

    private Quaternion initialRotation;

    void Start()
    {
        //pauseTime = 0f;            
        //selection = 0;            //for testing
        initialRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (basicBackandForth)
        {
            if (Time.time < pauseTime)
            {
                if (forward)
                {
                    transform.Rotate(0f, panSpeed * Time.deltaTime, 0f);
                }
                else
                {
                    transform.Rotate(0f, -panSpeed * Time.deltaTime, 0f);
                }
                
            }

            if (Time.time > pauseTime + pauseDuration)
            {
                forward = !forward;
                pauseTime = Time.time + panDuration;
            }





        }

        if (square)
        {
            if (Time.time < pauseTime)
            {
                if (selection == 0)
                {
                    transform.Rotate(panSpeedHorizontal * Time.deltaTime, 0f , 0f);
                }
                if (selection == 1)
                {
                    transform.Rotate(0f, 0f, panSpeedVertical * Time.deltaTime);
                }
                if (selection == 2)
                {
                    transform.Rotate(-panSpeedHorizontal * Time.deltaTime, 0f, 0f);
                }
                if (selection == 3)
                {
                    transform.Rotate(0f, 0f, -panSpeedVertical * Time.deltaTime);
                }
                // Can add more points here if want
                

            }
            if (Time.time > pauseTime + pauseDuration)
            {
                selection++;
                if (selection > 3)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, initialRotation, smoothRotationSpeed * Time.deltaTime);
                    selection = 0;
                }
                
                pauseTime = Time.time + panDuration;
            }
        }

        if (circular)
        {
            float angle = Time.time * panSpeed;
            float x = Mathf.Cos(angle) * circleRadius;
            float z = Mathf.Sin(angle) * circleRadius;
            /*
            transform.position = new Vector3(x, transform.position.y, z);
            transform.LookAt(Vector3.zero);  // This code will move the camera in a circle, usually not wanted     */ 

            Vector3 circularDirection = new Vector3(x, 0f, z);
            transform.LookAt(target.position + circularDirection);
        }

        if (manual)
        {
            transform.LookAt(target.position);
        }
    }
}
