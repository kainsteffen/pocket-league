using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleKiller : MonoBehaviour {


	public float deathTimer;
	// Use this for initialization
	void Start () {
		deathTimer = 4;
		
	}
	
	// Update is called once per frame
	void Update () {
		deathTimer -= Time.deltaTime;

		if (deathTimer < 0)
		{
			Destroy (gameObject);
		}
	}
}
