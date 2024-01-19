using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public float speedLerp;
    public Vector2 turn;
    private float sensitivity = 7f;
    public Rigidbody rb;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            turn.x += Input.GetAxis("Mouse X") * sensitivity;
            //turn.y += Input.GetAxis("Mouse Y") * sensitivity;
            transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0);
        }

        /* This actor is necessary because spheres don't rotate properly, and this way the camera can follow the rotation of a cube which is following the rotation of the player based on the player's velocity
         without having to change the player's rotation itself  */
        float step = speedLerp * Time.deltaTime;
        transform.position = player.transform.position;
        //Quaternion playerRotation = player.transform.rotation;
        Rigidbody rb = player.GetComponent<Rigidbody>();
        Vector3 speed = new Vector3 (rb.velocity.x, 0, rb.velocity.z);

        if (Mathf.Abs(rb.velocity.x) < .2 && Mathf.Abs(rb.velocity.z) < .2)
        {

        }
        else
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(speed), step);        // Makes the camera a bit smoother
        }
         
        //transform.rotation = Quaternion.LookRotation(speed);
        



    }

    private void LateUpdate()
    {
        /*if (Input.GetKey(KeyCode.RightArrow))        // Manual camera
        {
            transform.Rotate(new Vector3(0f, -.5f, 0f));
            player.transform.Rotate(new Vector3(0f, -.5f, 0f));
        }

        if (Input.GetKey(KeyCode.LeftArrow))        // Manual camera
        {
            transform.Rotate(new Vector3(0f, .5f, 0f));
            player.transform.Rotate(new Vector3(0f, .5f, 0f));
        }*/
    }


}
