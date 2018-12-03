using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public GameObject[] groups;

	public Color[] blockColors;

	public bool gameOver = false;

	private Group nextUp;
	private Group held;

	public GameObject nextUpSlot;
	public GameObject heldSlot;

	public void StartGame()
	{
		this.spawnNext();
		this.nextPiece();
	}

	public void GameOver()
	{
		gameOver = true;
	}

	public void SwapWithHeld(Group piece)
	{
		piece.disablePiece();
		foreach (Block item in piece.blocks)
		{
			Vector2 index = TetrisGrid.roundVec2(item.transform.position);
			TetrisGrid.removeBlock((int)index.x, (int)index.y);
		}

		if(held == null)
		{
			nextUp.pieceHasBeenHeld = true;
			this.nextPiece();
		}
		else 
		{
			held.transform.position = piece.transform.position;
			held.enablePiece();
		}

		held = piece;
		held.transform.position = (Vector2)heldSlot.transform.position + held.nextUpOffset;
	}

	public void nextPiece()
	{
		if(gameOver == false)
		{
			nextUp.transform.position = this.transform.position;
			nextUp.enablePiece();
			this.spawnNext();
		}
	}

	private void spawnNext() 
	{
		// Random Index
		int i = Random.Range(0, groups.Length);

		// Spawn Group at current Position
		GameObject nextUpPiece = Instantiate(groups[i], this.transform.position, Quaternion.identity);
		nextUp = nextUpPiece.GetComponent<Group>();
		nextUp.transform.position = (Vector2)nextUpSlot.transform.position + nextUp.nextUpOffset;
	}

	// public Color randomColor()
	// {
	// 	int colorIndex = Random.Range(0, this.blockColors.Length);
	// 	return this.blockColors[colorIndex];
	// }
}
