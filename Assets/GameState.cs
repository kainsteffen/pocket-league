using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;

public class GameState : MonoBehaviour {

	// Use this for initialization

	public GameObject seeker;
	public GameObject ball;
	public GameObject currentBall;

	public int score1 = 0;
	public int score2 = 0;
	public Text scoreText;

	public float slowDownFactor = 0.05F;
	public float slowDownDuration = 2F;
	public float slowDownTimer;
	public bool slowedDown;

	public string quickChat = "What a goal!";

	void Start () {

        seeker = Resources.Load ("seeker") as GameObject;
		ball = Resources.Load ("Ball") as GameObject;

		currentBall = GameObject.Find ("Ball");

		slowDownTimer = slowDownDuration;

	}
	
	// Update is called once per frame
	void Update () {


		displayScore ();

		if (slowedDown) 
		{
			normalizeTime ();
		}
			

	}	

	void Awake() 
	{
		Application.targetFrameRate = 60;
	}

	public void slowDownTime()
	{
		Time.timeScale = slowDownFactor;
		Time.fixedDeltaTime = Time.timeScale * 0.02F;
		slowedDown = true;
	}

	public void addScore (int team)
	{
		switch (team) 
		{
			case 1: 
				score1++;
				break;

			case 2:
				score2++;
				break;

		}
	}

	public void spawnBall()
	{
		currentBall = Instantiate (ball);
		currentBall.transform.position = new Vector3 (0, 0, 0);
	}

	void displayScore()
	{
		if (slowDownTimer >= 2) {
			scoreText.text = score1 + " : " + score2;
		} 
		else 
		{
			scoreText.text = quickChat;
		}
			
	}

	void normalizeTime ()
	{
		
		Time.timeScale += (1F / slowDownDuration) * Time.unscaledDeltaTime; //because deltaTime is affected when timeScale is changed
		Time.timeScale = Mathf.Clamp (Time.timeScale, 0f, 1f);

		slowDownTimer -= Time.unscaledDeltaTime;


		if (slowDownTimer < 0) 
		{
			Time.fixedDeltaTime = 0.02f;
			slowedDown = false;
			slowDownTimer = 2;

			currentBall.transform.position = new Vector3 (0,0,0);
			currentBall.SetActive (true);

			var temp = Random.Range (1,9);

			switch (temp) 
			{
			case 1: 
				quickChat = "What a goal!";
				break;

			case 2:
				quickChat = "Nice Shot!";
				break;

			case 3:
				quickChat = "Calculated.";
				break;

			case 4:
				quickChat = "Whew.";
				break;

			case 5:
				quickChat = "Holy cow!";
				break;

			case 6:
				quickChat = "Siiiick!";
				break;

			case 7:
				quickChat = "No Way!";
				break;

			case 8:
				quickChat = "Savage!";
				break;

			}

		}

	}



}
