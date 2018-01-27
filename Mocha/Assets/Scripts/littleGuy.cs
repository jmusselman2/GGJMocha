using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class littleGuy: MonoBehaviour {

	public int moveNum; 
	public float speed; 


	//Movement Variables
	private Vector2 newPos;
	private Vector2 currPos;
	private Vector2 myPos;
	private Vector2 midPos; 
	private float fraction;
	private float journeyLength;
	private float startTime; 
	public bool canMove; 

	//control variables
	public bool hasControl;

	//Collision Variables 



	// Use this for initialization
	void Start () {
		moveNum = 20;
		speed = 10;
		canMove = true;

	}
	
	// Update is called once per frame
	void Update () {
		//only move if the player has control of the unit 
		if (hasControl) {
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
			
	}

	void MoveLeft()
	{
		currPos = transform.position;
		newPos = new Vector2 (transform.position.x - moveNum, transform.position.y);
		midPos = new Vector2 (transform.position.x - moveNum / 2, transform.position.y);
		journeyLength = Vector2.Distance (currPos, newPos);
		startTime = Time.time;
		fraction = 0;
		canMove = false;
	}

	void MoveRight()
	{
		currPos = transform.position;
		newPos = new Vector2 (transform.position.x + moveNum, transform.position.y);
		midPos = new Vector2 (transform.position.x + moveNum/2, transform.position.y);
		journeyLength = Vector2.Distance (currPos, newPos);
		startTime = Time.time;
		fraction = 0;
		canMove = false;
	}

	void MoveUp()
	{
		currPos = transform.position;
		newPos = new Vector2 (transform.position.x, transform.position.y + moveNum);
		midPos = new Vector2 (transform.position.x , transform.position.y + moveNum/2);
		journeyLength = Vector2.Distance (currPos, newPos);
		startTime = Time.time;
		fraction = 0;
		canMove = false;
	}

	void MoveDown()
	{
		currPos = transform.position;
		newPos = new Vector2 (transform.position.x, transform.position.y - moveNum);
		midPos = new Vector2 (transform.position.x , transform.position.y - moveNum/2);
		journeyLength = Vector2.Distance (currPos, newPos);
		startTime = Time.time;
		fraction = 0;
		canMove = false;
	}


	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "wall" || other.gameObject.tag == "alien") {
			myPos = transform.position;
			if (compareVector(myPos, currPos)) {
				newPos = currPos;
			} else if (compareVector(myPos, midPos)) {
				newPos = midPos;
			}
			currPos = transform.position;
		}

	}

	bool compareVector(Vector2 v1, Vector2 v2)
	{
		float xDif = Mathf.Abs(v1.x - v2.x);
		float yDif = Mathf.Abs (v1.y - v2.y);


		if (xDif < 1 && yDif < 1) {
			return true;
		} 
		return false;

	}
}
