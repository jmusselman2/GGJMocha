using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tutorialScript : MonoBehaviour {

	public Canvas canvas;
	public GameObject[] areas;
	public Text text;

	public GameObject win;
	private wincondition winCondition; 

	private tutorialtrigger[] triggers;


	// Use this for initialization
	void Start () {
		triggers = new tutorialtrigger[areas.Length];
		getTriggers ();
		winCondition = win.GetComponent<wincondition> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (triggers [0].isArea) {
			text.text = "Oh No! You've crash landed on a deserted planet! \n It looks like your friend needs to be woken up. \n Use WASD to move to your friend.";
		} else if (triggers [1].isArea) {
			text.text = "Now that everyone is awake, you can get to the escape pods! \n Make sure that each bot gets its own one!"; 
		} else if (winCondition.hasWon) {
			text.text = "You did it! Now go wake up the rest of the bots so you can get back home! \n press any key to continue";
			if (Input.anyKeyDown) {
				Application.LoadLevel ("Level1");
			}
		}
		
		
	}

	void getTriggers()
	{
		for (int i = 0; i<areas.Length;i++)
		{
			triggers [i] = areas [i].GetComponent<tutorialtrigger> ();
		}
	}
}
