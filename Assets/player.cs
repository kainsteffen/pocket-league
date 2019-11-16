using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {


	float speed;
	float rotationSpeed;
	float jumpForce;
	int jumpCount;
	int jumpCountDefault;
	int direction;


	public Rigidbody2D rigidBody;
	GameObject bullet;
	GameObject seekerBomb;
	GameObject jumpParticle;
	// Use this for initialization


	void Start () {
		speed = 200;
		jumpForce = 150;
		rotationSpeed = 100;
		jumpCount = 2;
		jumpCountDefault = 2;
		direction = 1;

		rigidBody = GetComponent<Rigidbody2D> ();

		bullet = Resources.Load ("bullet") as GameObject;
		seekerBomb = Resources.Load ("seekerBomb") as GameObject;
		jumpParticle = Resources.Load ("ringParticle") as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
		float axis;
		if (Input.touchCount > 0 && 
			Input.GetTouch(0).phase == TouchPhase.Moved) {

			// Get movement of the finger since last frame
			Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
			touchDeltaPosition.y = 1;

			if (touchDeltaPosition.x > 0) {
				axis = 1;
			} else {
				axis = -1;
			}

			// Move object across XY plane
			rigidBody.AddForce (touchDeltaPosition * 50);
			rigidBody.AddTorque (-axis * rotationSpeed);
		}

		move ();
		jump ();
		



			//rigidBody.velocity = new Vector2(0, jumpForce);


		if (Input.GetMouseButtonDown (0)) 
		{
			//GameObject bulletClone = Instantiate (seekerBomb,transform.position + direction * new Vector3(1,0,0), transform.rotation);
			//bulletClone.GetComponent<Rigidbody2D> ().AddForce (new Vector2(direction * 1000,0));
		}
			

	}

	void move()
	{
		var axisForce = Input.GetAxis ("Horizontal");
		if (axisForce > 0.5F|| axisForce < -0.5F)
		{
			direction = Mathf.RoundToInt (axisForce);
		}
			
		rigidBody.AddForce (new Vector2(axisForce*speed, 50));
		rigidBody.AddTorque (-axisForce * rotationSpeed);
	}

	void jump()
	{
		var axisForce = Input.GetAxis ("Horizontal");
		if (Input.GetKeyDown (KeyCode.Space) && jumpCount > 0 || Input.GetMouseButtonDown (1)  && jumpCount > 0) 
		{
			if (jumpCount == 1) 
			{
				//rigidBody.AddForce (new Vector2 (0, (1.2F * jumpForce)), ForceMode2D.Impulse);

				if(rigidBody.velocity.magnitude * 3 < 70)
					rigidBody.velocity *= 3;
				Debug.Log (rigidBody.velocity.magnitude);
				rigidBody.AddTorque (-axisForce * 10000);


				jumpCount--;
				Instantiate (jumpParticle).transform.position = transform.position;


				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				if (Physics.Raycast (ray))
					Instantiate (jumpParticle).transform.position = Input.mousePosition;

			} 
			else 
			{
				rigidBody.AddForce (new Vector2 (0, jumpForce), ForceMode2D.Impulse);
				jumpCount--;

			}
		}

	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "structure" || collision.gameObject.tag == "Player") 
		{
			jumpCount = jumpCountDefault;

		}

	}
}
