using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementStuff : MonoBehaviour
{
    // Start is called before the first frame update
    public float panSpeed;
    public float circleRadius;
    private Vector3 initPosition;

    public bool circle;
    public bool vertical;
    public bool horizontal;
    public float pauseDur;
    public bool forward;
    public float panDur;

    private float pauseTime; 
    void Start()
    {
        initPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (circle)
        {
            float angle = Time.time * panSpeed;
            float x = Mathf.Cos(angle) * circleRadius;
            float z = Mathf.Sin(angle) * circleRadius;
            transform.position = new Vector3(x + initPosition.x, transform.position.y, z + initPosition.z);
        }

        if (vertical)
        {

            if (Time.time < pauseTime)
            {
                if (forward)
                {
                    Vector3 pos = transform.position;
                    pos += new Vector3 (panSpeed * Time.deltaTime, 0, 0);
                    transform.position = pos;
                }
                else
                {
                    Vector3 pos = transform.position;
                    pos += new Vector3(-panSpeed * Time.deltaTime, 0, 0);
                    transform.position = pos;
                }

            }

            if (Time.time > pauseTime + pauseDur)
            {
                forward = !forward;
                pauseTime = Time.time + panDur;
            }
        }

        if (horizontal)
        {

            if (Time.time < pauseTime)
            {
                if (forward)
                {
                    Vector3 pos = transform.position;
                    pos += new Vector3(0,0,panSpeed * Time.deltaTime);
                    transform.position = pos;
                }
                else
                {
                    Vector3 pos = transform.position;
                    pos += new Vector3(0, 0, -panSpeed * Time.deltaTime);
                    transform.position = pos;
                }

            }

            if (Time.time > pauseTime + pauseDur)
            {
                forward = !forward;
                pauseTime = Time.time + panDur;
            }
        }
    }
}
