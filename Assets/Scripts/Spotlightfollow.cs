using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spotlightfollow : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject player;
    private Vector3 offset;
    void Start()
    {
        offset = new Vector3(0,.5f,0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + offset;
    }
}
