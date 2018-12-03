using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group : MonoBehaviour 
{
	float lastFall = 0;
	float lastMove = 0;
	float lastPressedDown = 0;

	int fallen = 0;
	public bool pieceHasBeenHeld = false;

	public Vector2 nextUpOffset;
	private bool pieceEnabled = false;
	public Block[] blocks;

	void Start () 
	{
		startPiece();
	}
	
	void Update ()
	{
		UpdatePiece();
	}

	protected void startPiece()
	{
		blocks = this.GetComponentsInChildren<Block>();

		int randomRotate = Random.Range(0, 5);
		for (int i = 0; i < randomRotate; i++)
		{
			this.rotate();
		}
	}

	public void disablePiece()
	{
		pieceEnabled = false;
	}

	public void enablePiece()
	{
		pieceEnabled = true;
		if (!isValidGridPos()) 
		{
			Debug.Log("GAME OVER");
			GameManager.instance.gameOver();
		}
	}


	protected void UpdatePiece()
	{
		if(pieceEnabled == false)
		{
			return;
		}

		if(GameManager.instance == null)
		{
			return;
		}

		if(Input.GetKeyDown(KeyCode.RightControl) && this.fallen < 3 && this.pieceHasBeenHeld == false)
		{
			this.pieceHasBeenHeld = true;
			FindObjectOfType<Spawner>().SwapWithHeld(this);
		}
		else if (Input.GetKey(KeyCode.LeftArrow) && Time.time - lastMove >= 0.075) 
		{
			transform.position += new Vector3(-1, 0, 0);
			if (isValidGridPos())
			{
				updateGrid();
			}
			else
			{
				transform.position += new Vector3(1, 0, 0);
			}

			lastMove = Time.time;
		}
		else if (Input.GetKey(KeyCode.RightArrow) && Time.time - lastMove >= 0.075) 
		{
			transform.position += new Vector3(1, 0, 0);
			if (isValidGridPos())
			{
				updateGrid();
			}
			else
			{
				transform.position += new Vector3(-1, 0, 0);
			}

			lastMove = Time.time;
		}
		else if (Input.GetKeyDown(KeyCode.UpArrow)) 
		{
			this.rotate();
		}
		else if (Input.GetKey(KeyCode.DownArrow) && Time.time - lastPressedDown >= 0.075)
		{
			fall(true);
		}
		else if(Time.time - lastFall >= GameManager.instance.fallSpeed) 
		{
			fall(false);
		}
	}

	protected virtual void fall(bool pressed)
	{
		transform.position += new Vector3(0, -1, 0);

		if (isValidGridPos()) 
		{
			updateGrid();
			this.fallen++;
		} 
		else 
		{
			transform.position += new Vector3(0, 1, 0);

			SoundManager.instance.PlayPlaced();

			TetrisGrid.deleteFullRows();
			FindObjectOfType<Spawner>().nextPiece();

			pieceEnabled = false;
			foreach (Block block in this.blocks) 
			{
				block.placePieceOnBoard();
			}
		}

		lastFall = Time.time;
		if(pressed)
		{
			lastPressedDown = Time.time;
			lastFall += 0.25f;
		}
	}

	public virtual void rotate()
	{
		transform.Rotate(0, 0, -90);
		if (isValidGridPos())
		{
			SoundManager.instance.PlayRotate();
			TetrisGrid.LockRandomPiece();

			updateGrid();
		}
		else
		{
			transform.Rotate(0, 0, 90);
		}
	}

	protected void updateGrid() 
	{
		for (int y = 0; y < TetrisGrid.height; ++y)
		{
			for (int x = 0; x < TetrisGrid.width; ++x)
			{
				if (TetrisGrid.grid[x, y] != null)
				{
					if (TetrisGrid.grid[x, y].transform.parent == transform)
					{
						TetrisGrid.grid[x, y] = null;
					}
				}
			}
		}

		foreach (Block block in this.blocks) 
		{
			Vector2 v = TetrisGrid.roundVec2(block.transform.position);
			TetrisGrid.grid[(int)v.x, (int)v.y] = block;
		}        
	}

	protected bool isValidGridPos() 
	{
		bool valid = true;  
		foreach (Transform child in transform) 
		{
			Vector2 v = TetrisGrid.roundVec2(child.position);

			if (TetrisGrid.insideBorder(v) == false)
			{
				valid = false;
				break;
			}

			int width = TetrisGrid.grid.GetLength(0);
			int height = TetrisGrid.grid.GetLength(1);
			int x = (int)v.x;
			int y = (int)v.y;
			if((width > x) && (height > y))
			{
				if (TetrisGrid.grid[x,y] != null)
				{
					if(TetrisGrid.grid[(int)v.x, (int)v.y].transform.parent != transform)
					{
						valid = false;
						break;
					}
				}
			}
			else 
			{
				valid = false;
				break;
			}
		}
		return valid;
	}
}
