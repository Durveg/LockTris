using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group : MonoBehaviour {

	// Time since last gravity tick
	float lastFall = 0;
	float lastMove = 0;

	public Vector2 nextUpOffset;
	private bool pieceEnabled = false;
	private Block[] blocks;
	// Use this for initialization
	void Start () 
	{
		// Default position not valid? Then it's game over
		startPiece();
	}
	
	// Update is called once per frame
	void Update ()
	{
		UpdatePiece();
	}

	protected void startPiece()
	{
		blocks = this.GetComponentsInChildren<Block>();

		Color color = FindObjectOfType<Spawner>().randomColor();
		foreach (Block block in this.blocks)
		{
			block.spriteColor = color;
		}

		int randomRotate = Random.Range(0, 5);
		for (int i = 0; i < randomRotate; i++)
		{
			this.rotate();
		}
	}

	public void enablePiece()
	{
		pieceEnabled = true;
		if (!isValidGridPos()) {
			Debug.Log("GAME OVER");
			Destroy(gameObject);
		}
	}


	protected void UpdatePiece()
	{
		if(pieceEnabled == false)
			return;

				// Move Left
		if (Input.GetKey(KeyCode.LeftArrow) && Time.time - lastMove >= 0.1) {
			// Modify position
			transform.position += new Vector3(-1, 0, 0);
			
			// See if valid
			if (isValidGridPos())
				// Its valid. Update grid.
				updateGrid();
			else
				// Its not valid. revert.
				transform.position += new Vector3(1, 0, 0);

			lastMove = Time.time;
		}
		// Move Right
		else if (Input.GetKey(KeyCode.RightArrow) && Time.time - lastMove >= 0.1) {
			// Modify position
			transform.position += new Vector3(1, 0, 0);
			
			// See if valid
			if (isValidGridPos())
				// It's valid. Update grid.
				updateGrid();
			else
				// It's not valid. revert.
				transform.position += new Vector3(-1, 0, 0);

			lastMove = Time.time;
		}
		// Rotate
		else if (Input.GetKeyDown(KeyCode.UpArrow)) 
		{
			this.rotate();
		}
				// Fall
		else if (Input.GetKey(KeyCode.DownArrow) && Time.time - lastFall >= 0.1)
		{
			fall();
		}
		else if(Time.time - lastFall >= 10) 
		{
			fall();
		}
	}

	protected virtual void fall()
	{
		// Modify position
		transform.position += new Vector3(0, -1, 0);

		// See if valid
		if (isValidGridPos()) {
			// It's valid. Update grid.
			updateGrid();
		} else {
			// It's not valid. revert.
			transform.position += new Vector3(0, 1, 0);

			// Clear filled horizontal lines
			Grid.deleteFullRows();

			// Spawn next Group
			FindObjectOfType<Spawner>().nextPiece();

			pieceEnabled = false;
			foreach (Block block in this.blocks) 
			{
				block.SetLocked();
			}
		}

		lastFall = Time.time;
	}

	public virtual void rotate()
	{
		transform.Rotate(0, 0, -90);
		// See if valid
		if (isValidGridPos())
		{
			// It's valid. Update grid.
			updateGrid();
			Grid.LockRandomPiece();
		}
		else
		{
			transform.Rotate(0, 0, 90);
		}
	}

	protected void updateGrid() 
	{
		// Remove old children from grid
		for (int y = 0; y < Grid.height; ++y)
			for (int x = 0; x < Grid.width; ++x)
				if (Grid.grid[x, y] != null)
					if (Grid.grid[x, y].transform.parent == transform)
						Grid.grid[x, y] = null;

		// Add new children to grid
		foreach (Block block in this.blocks) 
		{
			Vector2 v = Grid.roundVec2(block.transform.position);
			Grid.grid[(int)v.x, (int)v.y] = block;
		}        
	}

	protected bool isValidGridPos() 
	{
		bool valid = true;  
		foreach (Transform child in transform) 
		{
			Vector2 v = Grid.roundVec2(child.position);

			// Not inside Border?
			if (Grid.insideBorder(v) == false)
			{
				valid = false;
				break;
			}

			// Block in grid cell (and not part of same group)?
			int width = Grid.grid.GetLength(0);
			int height = Grid.grid.GetLength(1);
			int x = (int)v.x;
			int y = (int)v.y;

			if((width > x) && (height > y))
			{
				if (Grid.grid[x,y] != null)
				{
					if(Grid.grid[(int)v.x, (int)v.y].transform.parent != transform)
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
