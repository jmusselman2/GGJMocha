using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class littleGuy: MonoBehaviour {

	public int moveNum; 
	public float speed; 

	private Vector2 newPos;
	private Vector2 currPos;
	private Vector2 myPos;
	private float fraction;
	private float journeyLength;
	private float startTime; 
	private bool canMove; 


	// Use this for initialization
	void Start () {
		moveNum = 10;
		speed = 10;
		canMove = true;

	}
	
	// Update is called once per frame
	void Update () {
		//Prevent player from moving before the units reach the end of their travel
		if (canMove) {
			if (Input.GetKeyDown (KeyCode.A)) {
				MoveLeft ();
			} else if (Input.GetKeyDown (KeyCode.D)) {
				MoveRight ();
			} else if (Input.GetKeyDown (KeyCode.S)) {
				MoveDown ();
			} else if (Input.GetKeyDown (KeyCode.W)) {
				MoveUp ();
			}
		} else {
			if (fraction < 1) {
				fraction = ((Time.time - startTime) * speed) / journeyLength;
				transform.position = Vector2.Lerp (currPos, newPos, fraction);
			}


		    myPos = transform.position;
			if (myPos == newPos) {
				canMove = true;
			}
		}

	}

	void MoveLeft()
	{
		currPos = transform.position;
		newPos = new Vector2 (transform.position.x - moveNum, transform.position.y);
		journeyLength = Vector2.Distance (currPos, newPos);
		startTime = Time.time;
		fraction = 0;
		canMove = false;
	}

	void MoveRight()
	{
		currPos = transform.position;
		newPos = new Vector2 (transform.position.x + moveNum, transform.position.y);
		journeyLength = Vector2.Distance (currPos, newPos);
		startTime = Time.time;
		fraction = 0;
		canMove = false;
	}

	void MoveUp()
	{
		currPos = transform.position;
		newPos = new Vector2 (transform.position.x, transform.position.y + moveNum);
		journeyLength = Vector2.Distance (currPos, newPos);
		startTime = Time.time;
		fraction = 0;
		canMove = false;
	}

	void MoveDown()
	{
		currPos = transform.position;
		newPos = new Vector2 (transform.position.x, transform.position.y - moveNum);
		journeyLength = Vector2.Distance (currPos, newPos);
		startTime = Time.time;
		fraction = 0;
		canMove = false;
	}



}
