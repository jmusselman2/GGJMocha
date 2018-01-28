using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level1Script : MonoBehaviour {

	public GameObject win;
	private wincondition winCondition; 

	// Use this for initialization
	void Start () {
		winCondition = win.GetComponent<wincondition> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		if (winCondition.hasWon) {
			if (Input.anyKeyDown) {
				Application.LoadLevel ("Start");
			}
		}
		
	}
}
