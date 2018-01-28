using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wintile : MonoBehaviour {

	public bool isAlien; 

	// Use this for initialization
	void Start () {
		isAlien = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "alien") {
			isAlien = true;
		}
	}
}
