using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seekerBomb : MonoBehaviour {



	float countdown;
	int seekerCount;

	GameObject seeker;
	GameObject deathParticle;

	// Use this for initialization
	void Start () {
		seeker = Resources.Load("seeker") as GameObject;
		deathParticle = Resources.Load ("enemyDisintegrate") as GameObject;

		countdown = 5;
		seekerCount = 10;
	}
	
	// Update is called once per frame
	void Update () {

		countdown -= Time.deltaTime;
		if (countdown < 0) 
		{
			for (int i = 0; i < seekerCount; i++) 
			{
				GameObject seekerClone = Instantiate (seeker);
				seekerClone.transform.position = transform.position;
				Rigidbody2D rb = seekerClone.GetComponent<Rigidbody2D> ();




				rb.AddForce (new Vector2 (Random.Range (-1000, 1000), Random.Range (-1000, 1000)));

			}

			GameObject deathParticleClone = Instantiate (deathParticle);
			deathParticleClone.transform.position = transform.position;


			Destroy (gameObject);
		}
		
	}
}
