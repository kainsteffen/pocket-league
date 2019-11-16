using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyGoalie : MonoBehaviour {


	float speed;
	float rotationSpeed;
	float jumpForce;
	int jumpCount;
	int jumpCountDefault;


	public Rigidbody2D rigidBody;
	GameObject bullet;
	GameObject seekerBomb;
	GameObject jumpParticle;
	GameObject targetBall;
	// Use this for initialization
	void Start () {
		speed = 200;
		jumpForce = 300;
		rotationSpeed = 150;
		jumpCount = 2;
		jumpCountDefault = 2;

		rigidBody = GetComponent<Rigidbody2D> ();

		bullet = Resources.Load ("bullet") as GameObject;
		seekerBomb = Resources.Load ("seekerBomb") as GameObject;
		jumpParticle = Resources.Load ("ringParticle") as GameObject;

		targetBall = GameObject.Find("Ball");
	}

	// Update is called once per frame
	void Update () {

		if (Random.Range (1, 10000) == 1)
		{
			Instantiate (seekerBomb).transform.position = transform.position;
		}

		if (targetBall.GetComponent<Rigidbody2D> ().velocity.magnitude < 0.5 && rigidBody.velocity.magnitude < 0.5) 
		{
			move (10);
		}

		if(targetBall.transform.position.x > transform.position.x)
		{
			move (1F);
		}
		else
		{
			move(-1F);
		}

		if ((targetBall.transform.position.y - transform.position.y) < 3 && (targetBall.transform.position.x - transform.position.x) < 3) 
		{
			jump ();
		}

	}

	void move(float direction)
	{
		rigidBody.AddForce (new Vector2(direction*speed, 100));
		rigidBody.AddTorque (-direction * rotationSpeed);
	}

	void jump()
	{
		if (jumpCount == 2) {
			rigidBody.AddForce (new Vector2 (0, jumpForce), ForceMode2D.Impulse);
			jumpCount--;
		}
		else 
		{
			doubleJump ();
		}
			
	}

	void doubleJump()
	{
		if (jumpCount > 0)
		{
			var shotAngle = transform.position - targetBall.transform.position;
			rigidBody.AddForce (-shotAngle * 2, ForceMode2D.Impulse);
			rigidBody.AddTorque (Random.Range( -10000, 10000));
			jumpCount--;
			Instantiate (jumpParticle).transform.position = transform.position;
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "structure") 
		{
			jumpCount = jumpCountDefault;

		}

	}
}
