using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZGroup : Group 
{
	bool rotated = false;
	// Use this for initialization
	void Start () 
	{
		startPiece();
	}
	
	// Update is called once per frame
	void Update () 
	{
		UpdatePiece();	
	}

	public override void rotate()
	{
		if(rotated == false)
		{
			transform.Rotate(0, 0, -90);
			// See if valid
			if (isValidGridPos())
			{
				// It's valid. Update grid.
				updateGrid();
				rotated = true;
			}
			else
			{
				transform.Rotate(0, 0, 90);
			}
		}
		else
		{
			transform.Rotate(0, 0, 90);
			bool validPos = isValidGridPos();
			if (validPos == true)
			{
				// It's valid. Update grid.
				updateGrid();
				rotated = false;
			}
			else
			{
				transform.Rotate(0, 0, -90);
			}
		}
	}
}
