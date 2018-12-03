using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

	public static int width = 10;
	public static int height = 30;
	public static Block[,] grid = new Block[width, height];

	public static Vector2 roundVec2(Vector2 v) 
	{
    	return new Vector2(Mathf.Round(v.x),
        	               Mathf.Round(v.y));
	}

	public static bool insideBorder(Vector2 pos) 
	{
    	return ((int)pos.x >= 0 &&
        	    (int)pos.x < width &&
            	(int)pos.y >= 0);
	}

	public static void deleteRow(int y) 
	{
    	for (int x = 0; x < width; ++x) {
			if(grid[x, y].pieceLockedValue == 0)
			{
       			Destroy(grid[x, y].gameObject);
        		grid[x, y] = null;
			}
			else 
			{
				grid[x, y].UnlockPiece();
			}
    	}
	}

	public static void decreaseRow(int y) 
	{
		for (int x = 0; x < width; ++x) {
			if (grid[x, y] != null) {

				if(grid[x, y-1] == null)
				{
					// Move one towards bottom
					grid[x, y-1] = grid[x, y];
					grid[x, y] = null;

					// Update Block position
					grid[x, y-1].transform.position += new Vector3(0, -1, 0);
				}
			}
		}
	}

	public static void LockRandomPiece()
	{
		List<Block> lockedPieces = new List<Block>();
		for(int y = 0; y < height; y++)
		{
			for(int x = 0; x < width; x++)
			{
				if(grid[x, y] != null)
				{
					if(grid[x, y].pieceLockedValue == 0 && grid[x, y].placedOnBoard == true)
					{
						lockedPieces.Add(grid[x, y]);
					}
				}
			}
		}

		if(lockedPieces.Count > 0)
		{
			int locked = Random.Range(0, lockedPieces.Count);
			lockedPieces[locked].LockPiece();
		}
	}

	public static void decreaseRowsAbove(int y) 
	{
    	for (int i = y; i < height; ++i)
		{
        	decreaseRow(i);
		}
	}

	public static bool isRowFull(int y) 
	{
    	for (int x = 0; x < width; ++x)
        	if (grid[x, y] == null)
            	return false;
    	return true;
	}
	public static void deleteFullRows() 
	{
		for (int y = 0; y < height; ++y) 
		{
			if (isRowFull(y)) 
			{
				deleteRow(y);
				decreaseRowsAbove(y+1);
				--y;
			}
		}
	}
}
