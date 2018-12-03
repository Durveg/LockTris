using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class squareGroup : Group {

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
		
	}
}
