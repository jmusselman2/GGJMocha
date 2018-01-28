using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wintile : MonoBehaviour {

	public bool isAlien;
	public bool isOnGrid;
	public bool isWin; 

	public GameObject alien; 

	// Use this for initialization
	void Start () {
		isAlien = false;
		isOnGrid = false;
		isWin = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (isAlien) {
			if (alien != null) {
				
			}
		}

		if (isOnGrid & isAlien) {
			isWin = true; 
		}
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "alien") {
			alien = other.gameObject;
			isAlien = true;
		}
	}
	void OnTriggerExit2D (Collider2D other)
	{
		if (other.gameObject.tag == "alien") {
			isAlien = false;
		}
	}


}
