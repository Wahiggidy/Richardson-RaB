using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;
	public Transform playerT;
    private Transform playerTransform = null;


    [Range(1, 10)]
    [SerializeField] private float lerpSpeed = 5f;
    [SerializeField] public Vector3 offset;
    [SerializeField] private Vector3 minValue, maxValue;

	
	void Start ()
    {
        //offset = transform.position - player.transform.position;
		playerTransform = player.transform;
	}
	
	
	void FixedUpdate ()
    {

		//Quaternion origRotation = playerTransform.rotation;

		Vector3 endPos = playerTransform.TransformPoint(offset);
        //Vector3 endPos = player.transform.position + offset;

        Vector3 clampPos = new Vector3(
			Mathf.Clamp(endPos.x, minValue.x, maxValue.x),
			Mathf.Clamp(endPos.y, minValue.y, maxValue.y),
			Mathf.Clamp(endPos.z, minValue.z, maxValue.z));


		Vector3 smoothPos = Vector3.Lerp(
			transform.position,
			clampPos,
			lerpSpeed * Time.deltaTime);
			
			transform.position = smoothPos;
			//Debug.Log(clampPos);





		transform.LookAt(playerTransform);


		//transform.position = player.transform.position + offset;
		//transform.LookAt(player.transform.position);
	}
}
