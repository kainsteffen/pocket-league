using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ball : MonoBehaviour {


	public GameObject goal1;
	public GameObject goal2;
	public GameObject deathParticle;
	public GameState gameState;


	public Rigidbody2D rigidBody;

	// Use this for initialization
	void Start () {

		goal1 = GameObject.Find ("goal1");
		goal2 = GameObject.Find ("goal2");

		rigidBody = GetComponent<Rigidbody2D> ();

		
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (rigidBody.velocity.magnitude);

	}
		


	void OnCollisionEnter2D(Collision2D collider)
	{

		if (collider.gameObject == goal2) 
		{
			

			Instantiate (deathParticle).transform.position = transform.position;
			gameState.slowDownTime ();
			gameState.addScore (1);

			gameObject.SetActive (false);


		} 

		if (collider.gameObject == goal1) 
		{
			Instantiate (deathParticle).transform.position = transform.position;
			gameState.slowDownTime ();
			gameState.addScore (2);

			gameObject.SetActive (false);


		}
	}
}
