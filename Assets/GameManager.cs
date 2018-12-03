using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
	public static GameManager instance;

	public float fallSpeed = 1;

	public int pointsPerRow = 100;
	public int pointsPerTetris = 800;

	public int totalPoints = 0;
	public int linesCleared = 0;

	private void Start() 
	{
		if(GameManager.instance == null)
		{
			GameManager.instance = this;
		}	
		else
		{
			GameObject.Destroy(this.gameObject);
		}
	}

	public void AddPoints(int multiplier)
	{
		totalPoints += multiplier * pointsPerRow;
		linesCleared += multiplier;
	}

	public void AddTetris()
	{
		totalPoints += pointsPerTetris;
		linesCleared += 4;
	}

	public void gameOver()
	{
		SoundManager.instance.PlayGameOver();
		FindObjectOfType<Spawner>().GameOver();
		FindObjectOfType<GameUI>().GameOver();
		//ToDo: Show GameOverUI.
	}
}
