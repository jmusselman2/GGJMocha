using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class littleGuy : MonoBehaviour {

	public Animator anim;
	public int moveNum;
	public float speed;

	// Movement Variables
	private Vector2 newPos;
	private Vector2 currPos;
	private Vector2 myPos;
	private Vector2 midPos; 
	private float fraction;
	private float journeyLength;
	private float startTime; 
	public bool canMove; 

	// control variables
	public bool hasControl;

	// Collision Variables 
	private littleGuy otherAlien; 
	public bool setControl;

	// Use this for initialization
	void Start () {
		moveNum = 20;
		speed = 20;
		canMove = true;
		setControl = false;
		anim = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () {

		// Only move if the player has control of the unit 
		if (hasControl) {

			// Always check for activating alien
			activateAlien();

			// Prevent player from moving before the units reach the end of their travel
			if (canMove) {

				// Makes sure that the aliens stay on the grid
				if (transform.position.x % 5 != 0) {
					float newX = Mathf.Round(transform.position.x);
					if ((Mathf.Round(transform.position.x) - 1) % 5 == 0) {
						newX = Mathf.Round (transform.position.x - 1);
					}
					else if ((Mathf.Round(transform.position.x) + 1) % 5 == 0) {
						newX = Mathf.Round (transform.position.x + 1);
					}
					transform.position = new Vector2 (newX, transform.position.y);
				}

				if (transform.position.y % 5 != 0) {
					float newY = Mathf.Round (transform.position.y);
					if ((Mathf.Round (transform.position.y) - 1) % 5 == 0) {
						newY = Mathf.Round (transform.position.y - 1);
					}
					else if ((Mathf.Round (transform.position.y) + 1) % 5 == 0) {
						newY = Mathf.Round (transform.position.y + 1);
					}

					transform.position = new Vector2 (transform.position.x, newY);
				}

				// Player input 
				if (Input.GetKeyDown (KeyCode.A) || Input.GetAxis("Joy X") < 0) {
					MoveLeft();
				}
				else if (Input.GetKeyDown(KeyCode.D) || Input.GetAxis("Joy X") > 0) {
					MoveRight();
				}
				else if (Input.GetKeyDown(KeyCode.S) || Input.GetAxis("Joy Y") < 0) {
					MoveDown();
				}
				else if (Input.GetKeyDown(KeyCode.W )|| Input.GetAxis("Joy Y") > 0) {
					MoveUp();
				}
			}
			else {
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

		anim.Play("LeftWalking");
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

		anim.Play("RightWalking");
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

		anim.Play("BackWalking");
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

		anim.Play("FrontWalking");
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "wall" || other.gameObject.tag == "alien") {
			//Debug.Log (other.gameObject.tag);
			myPos = transform.position;
			if (compareVector (myPos, currPos)) {
				newPos = currPos;
			}
			else if (compareVector (myPos, midPos)) {
				newPos = midPos;
			}
			else {
				Debug.Log ("Reset Loc Failed");
				//transform.position = new Vector2 (Mathf.Round (transform.position.x), Mathf.Round (transform.position.y));
			}
			currPos = transform.position;
		}
	}

	bool compareVector(Vector2 v1, Vector2 v2)
	{
		float xDif = Mathf.Abs(v1.x - v2.x);
		float yDif = Mathf.Abs (v1.y - v2.y);

		if (xDif < 5 && yDif < 5) {
			return true;
		}

		return false;
	}

	void activateAlien()
	{
		RaycastHit hit;
		//Debug.DrawRay (transform.position, transform.right * 8.0f, Color.green);

		Debug.DrawRay (transform.position, transform.right * 8.0f, Color.green);

		var layerMask = (1 << 8);







		RaycastHit2D leftHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.left, 6.0f, layerMask);
		RaycastHit2D rightHit = Physics2D.Raycast (new Vector2(transform.position.x, transform.position.y), Vector2.right, 6.0f, layerMask);
		RaycastHit2D upHit = Physics2D.Raycast (new Vector2(transform.position.x, transform.position.y), Vector2.up, 6.0f, layerMask);
		RaycastHit2D downHit = Physics2D.Raycast (new Vector2(transform.position.x, transform.position.y), Vector2.down, 6.0f, layerMask);

		if (leftHit.collider != null) {
			alienCollision (leftHit);
		}
		if (rightHit.collider != null) {
			alienCollision (rightHit);
		}
		if (upHit.collider != null) {
			alienCollision (upHit);
		}
		if (downHit.collider != null) {
			alienCollision (downHit);
		}


	}

	void alienCollision (RaycastHit2D hit)
	{
		Debug.Log (gameObject.name + " detects " + hit.collider.gameObject.name);
			
		if (hit.collider.gameObject.tag == "alien") {
			otherAlien = hit.collider.gameObject.GetComponent<littleGuy> ();
			otherAlien.hasControl = true;
		}
	}

}
