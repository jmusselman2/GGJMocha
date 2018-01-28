using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wincondition : MonoBehaviour {

	public GameObject[] winTiles;
	public GameObject[] dudes;
	public Animator[] anims;

	private wintile winTile;
	private bool[] boolArray;

	public bool hasWon;
	// Use this for initialization
	void Start () {
		boolArray = new bool[winTiles.Length];
		hasWon = false;

		anims = new Animator[dudes.Length];
		GetAnimators();
	}
	
	// Update is called once per frame
	void Update () {
		if (checkWinState (getWinState())) {
			hasWon = true;
			Debug.Log ("level won");

			foreach (var anim in anims) {
				Debug.Log("play Happy Hop");
				anim.Play("Happy Hop");
			}
		}
	}

	bool[] getWinState()
	{
		for (int i = 0; i < winTiles.Length; i++) {
			winTile = winTiles [i].GetComponent<wintile> ();
			boolArray[i] = winTile.isAlien;
		}
		return boolArray;
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
