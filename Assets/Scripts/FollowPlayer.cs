using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public float speedLerp;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /* This actor is necessary because spheres don't rotate properly, and this way the camera can follow the rotation of a cube which is following the rotation of the player based on the player's velocity
         without having to change the player's rotation itself  */
        float step = speedLerp * Time.deltaTime;
        transform.position = player.transform.position;
        //Quaternion playerRotation = player.transform.rotation;
        Rigidbody rb = player.GetComponent<Rigidbody>();
        Vector3 speed = new Vector3 (rb.velocity.x, 0, rb.velocity.z);

        
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(speed), step);        // Makes the camera a bit smoother 
        //transform.rotation = Quaternion.LookRotation(speed);


    }
}
