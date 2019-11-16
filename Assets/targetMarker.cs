using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetMarker : MonoBehaviour 
{


	Quaternion rotation;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.position = transform.parent.gameObject.transform.position;
		transform.position += new Vector3 (0, 1.2F, 0);
	}
		
	void Awake()
	{
		rotation = transform.rotation;
	}

	void LateUpdate()
	{
		transform.rotation = rotation;	
	}
}
