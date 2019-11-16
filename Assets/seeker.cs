using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seeker : MonoBehaviour {

	// Use this for initialization

	float rotationSpeed;
	float speed;
	float deathTimer;

	bool seeking;


	Rigidbody2D rigidBody;
	Transform target;

	void Start () 
	{

		rigidBody = GetComponent<Rigidbody2D> ();
		rotationSpeed = 5000F;
		speed = 5;
		deathTimer = 1;
		seeking = true;

		target = GameObject.Find ("player").transform;
		//transform.LookAt (target.transform);
		
	}
	
	// Update is called once per frame
	void Update () 
	{


		immobileDecay ();

		if (seeking)
		{
			seekTarget();
		}

	}

	void immobileDecay()
	{
		if (rigidBody.velocity.magnitude < 1) 
		{
			deathTimer -= Time.deltaTime;

		}

		if (deathTimer < 0) 
		{
			Destroy (gameObject);
		}
	}

	void seekTarget()
	{
		Vector2 direction = (Vector2)transform.position - (Vector2)target.position;
		direction.Normalize ();

		float zAxis = Vector3.Cross (direction, transform.up).z;

		rigidBody.angularVelocity = rotationSpeed * zAxis;

		rigidBody.velocity = transform.up * speed;
	}

	void OnCollisionEnter2D(Collision2D collider)
	{
		if (collider.gameObject.tag == "Player") 
		{
			collider.gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2(0,0);//AddForce (rigidBody.velocity * 1000);
			var joint = gameObject.AddComponent<FixedJoint2D>();
			joint.connectedBody = collider.gameObject.GetComponent<Rigidbody2D> ();
		}
		if (collider.gameObject.tag == "structure") 
		{
			var joint = gameObject.AddComponent<FixedJoint2D>();
			joint.connectedBody = collider.gameObject.GetComponent<Rigidbody2D> ();
			seeking = false;
		}

	}
}
