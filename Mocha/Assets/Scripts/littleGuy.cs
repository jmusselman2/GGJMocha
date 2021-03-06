﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

	//audio variables
	public AudioClip[] walkingAudio;
	public AudioClip transmission;
	AudioSource source;
	int random;
	public float clipLength;

	// Use this for initialization
	void Start () {
		moveNum = 20;
		speed = 20;
		canMove = true;
		setControl = false;
		anim = GetComponent<Animator>();

		source = GetComponent<AudioSource> ();
		random = 0;
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
				if (Input.GetKeyDown (KeyCode.A) || Input.GetAxis("Joy X") < 0 || Input.GetKey ("left")) {
					MoveLeft();

				}
				else if (Input.GetKeyDown(KeyCode.D) || Input.GetAxis("Joy X") > 0 || Input.GetKey ("right")) {
					MoveRight();

				}
				else if (Input.GetKeyDown(KeyCode.S) || Input.GetAxis("Joy Y") < 0 || Input.GetKey ("down")) {
					MoveDown();
				}
				else if (Input.GetKeyDown(KeyCode.W )|| Input.GetAxis("Joy Y") > 0 || Input.GetKey ("up")) {
					MoveUp();
				}
				else if (Input.GetKey(KeyCode.Escape)) {
					SceneManager.LoadScene("Start");
				}
			}
			else {
				if (fraction < 1) {
					fraction = ((Time.time - startTime) * speed) / journeyLength;
					transform.position = Vector2.Lerp (currPos, newPos, fraction);
					if (gameObject.name == "A1") {
						playWalkingSound();
					}
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
			myPos = transform.position;
			
			if (compareVector (myPos, currPos)) {
				newPos = currPos;
			}
			else if (compareVector (myPos, midPos)) {
				newPos = midPos;
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
		if (hit.collider.gameObject.tag == "alien") {
			otherAlien = hit.collider.gameObject.GetComponent<littleGuy> ();
			if (!otherAlien.hasControl)
			{
				Transform wave = this.gameObject.transform.GetChild(0);
				Animator waveComponent = wave.GetComponent<Animator>();
				waveComponent.Play("Waves");

				Animator otherDude = hit.collider.gameObject.GetComponent<Animator>();
				otherDude.Play("Get Up");

				source.PlayOneShot (transmission);
				otherAlien.hasControl = true;
			}
		}
	}

	void playWalkingSound()
	{
		if (!source.isPlaying) {
			random = Random.Range (0, 3);
			source.PlayOneShot (walkingAudio [random]);
		}
	}

}
