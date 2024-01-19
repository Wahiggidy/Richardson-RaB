using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

	void Update ()
    {
        transform.Rotate(new Vector3(0, 300, 0) * Time.deltaTime);       // Old values x=15 z=45
	}
}
