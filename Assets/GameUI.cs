using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class GameUI : MonoBehaviour 
{

	public TMP_Text pointsText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(GameManager.instance == null)
		{
			return;
		}	

		pointsText.text = "Points: " + GameManager.instance.totalPoints;
	}
}
