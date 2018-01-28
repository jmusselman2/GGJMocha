using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialtrigger : MonoBehaviour {

	public bool isArea;

	// Use this for initialization
	void Start () {
		isArea = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.name == "A1") {
			isArea = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.name == "A1") {
			isArea = false;
		}
		
	}

}
