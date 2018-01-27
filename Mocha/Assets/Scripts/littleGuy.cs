using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class littleGuy: MonoBehaviour {

	public int moveNum; 
	public float speed; 

	private Vector2 newPos;
	private bool canMove; 


	// Use this for initialization
	void Start () {
		moveNum = 10;
		speed = 2;
		canMove = true;

	}
	
	// Update is called once per frame
	void Update () {
		if (canMove) {
			if (Input.GetKeyUp (KeyCode.A)) {
			//	MoveLeft ();
				canMove = false;
			} else if (Input.GetKeyUp (KeyCode.D)) {
			//	MoveRight ();
				canMove = false;
			} else if (Input.GetKeyUp (KeyCode.S)) {
			//	MoveDown ();
				canMove = false;
			} else if (Input.GetKeyUp (KeyCode.W)) {
			//	MoveUp ();
				canMove = false;
			}
		}

	}

	void MoveLeft(Vector2 currPos)
	{
		newPos = new Vector2 (transform.position.x - moveNum, transform.position.y);
		transform.position = Vector2.Lerp (currPos, newPos, 10f);
	}

	void MoveRight(Vector2 currPos)
	{
		newPos = new Vector2 (transform.position.x + moveNum, transform.position.y);
		transform.position = Vector2.Lerp (currPos, newPos, 10f);
	}

	void MoveUp(Vector2 currPos)
	{
		newPos = new Vector2 (transform.position.x, transform.position.y + moveNum);
		transform.position = Vector2.Lerp (currPos, newPos, 10f);
	}

	void MoveDown(Vector2 currPos)
	{
		newPos = new Vector2 (transform.position.x, transform.position.y - moveNum);
		transform.position = Vector2.Lerp (currPos, newPos, 10f);
	}

	Vector2 SetStart()
	{
		return transform.position;
	}

}
