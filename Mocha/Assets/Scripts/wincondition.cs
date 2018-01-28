using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wincondition : MonoBehaviour {

	public GameObject[] winTiles;
	private wintile winTile;
	private bool[] boolArray;
	// Use this for initialization
	void Start () {
		boolArray = new bool[4];
	}
	
	// Update is called once per frame
	void Update () {
		if (checkWinState (getWinState())) {
			Debug.Log ("level won");
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


}
