using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementStuff : MonoBehaviour
{
    // Start is called before the first frame update
    public float panSpeed;
    public float circleRadius;
    private Vector3 initPosition;
    void Start()
    {
        initPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float angle = Time.time * panSpeed;
        float x = Mathf.Cos(angle) * circleRadius;
        float z = Mathf.Sin(angle) * circleRadius;
        transform.position = new Vector3(x + initPosition.x, transform.position.y, z + initPosition.z);
    }
}
