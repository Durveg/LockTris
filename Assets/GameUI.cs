using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class GameUI : MonoBehaviour 
{

	public TMP_Text pointsText;

	public CanvasGroup tutorialUI;
	public CanvasGroup inGameUI;
	public CanvasGroup gameOverUI;

	void start()
	{
		tutorialUI.interactable = true;
		tutorialUI.alpha = 1;
		
		inGameUI.alpha = 0;
		inGameUI.interactable = false;
	}

	public void StartGame()
	{
		tutorialUI.interactable = false;
		tutorialUI.alpha = 0;
		
		inGameUI.alpha = 1;
		inGameUI.interactable = true;

		FindObjectOfType<Spawner>().StartGame();

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
