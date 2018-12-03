using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour 
{

	public static GameUI instance;

	public TMP_Text pointsText;
	public TMP_Text linesCleared;
	
	public CanvasGroup tutorialUI;
	public CanvasGroup inGameUI;
	public CanvasGroup gameOverUI;

	void Start()
	{
		GameUI.instance = this;


		tutorialUI.alpha = 1;
		tutorialUI.interactable = true;

		inGameUI.alpha = 0;
		inGameUI.interactable = false;

		gameOverUI.alpha = 0;
		gameOverUI.interactable = false;
	}

	public void GameOver()
	{
		gameOverUI.alpha = 1;
		gameOverUI.interactable = true;
	}

	public void StartGame()
	{
		tutorialUI.interactable = false;
		tutorialUI.alpha = 0;
		
		inGameUI.alpha = 1;
		inGameUI.interactable = true;

		FindObjectOfType<Spawner>().StartGame();
	}

	public void Restart()
	{
		SceneManager.LoadScene(0);
	}

	// Update is called once per frame
	void Update () 
	{
		if(GameManager.instance == null)
		{
			return;
		}	

		pointsText.text = GameManager.instance.totalPoints.ToString();
		linesCleared.text = "Lines: " + GameManager.instance.linesCleared;
	}
}
