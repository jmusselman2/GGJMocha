using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wincondition : MonoBehaviour {

	public GameObject[] winTiles;
	public GameObject[] dudes;
	public Animator[] anims;

	private wintile winTile;
	private GameObject[] aliens; 
	private bool[] boolArray;

	public bool hasWon;
	// Use this for initialization
	void Start () {
		boolArray = new bool[winTiles.Length];
		hasWon = false;

		anims = new Animator[dudes.Length];
		aliens = new GameObject[winTiles.Length];
		GetAnimators();
	}
	
	// Update is called once per frame
	void Update () {
		if (getWinState()) {
			hasWon = true;
			//Debug.Log ("level won");

			foreach (var anim in anims) {
				//Debug.Log("play Happy Hop");
				anim.Play("Happy Hop");
			}
		}
	}

	bool getWinState()
	{
		for (int i = 0; i < winTiles.Length; i++) {
			winTile = winTiles [i].GetComponent<wintile> ();
			if (winTile.alien != null) {
				aliens [i] = winTile.alien;
			} else {
				aliens [i] = null;
			}

			//if(winTile.alien.name 

			//boolArray[i] = winTile.isWin;
		}

		for (int i = 0; i < aliens.Length; i++) {

			if (aliens [i] == null) {
				return false;
			}
			for (int j = i+1; j < aliens.Length; j++) {
				if (aliens [j] == null) {
					return false;
				}
				if (aliens [i].name == aliens [j].name) {
					return false;
				}
			}
		}
		return true;
	}

	bool checkWinState(bool[] winState)
	{
		for (int i = 0; i < winState.Length; i++) {
			if (winState [i] == false) {
				return false;
			}
		}
		return true;
	}
		

	void GetAnimators() {
		for (int i = 0; i < dudes.Length; i++) {
			anims[i] = dudes[i].GetComponent<Animator>();
		}
	}
			
	
}
