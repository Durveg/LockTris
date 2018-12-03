using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour 
{
	public SpriteRenderer spriteRenderer;
	public Sprite unlockedSprite;
	public Sprite singleLockSprite;
	public Sprite doubleLockSprite;

	public int pieceLockedValue = 0;

	public Color spriteColor;
	public bool placedOnBoard = false;

	public void placePieceOnBoard()
	{
		placedOnBoard = true;
	}

	public void LockPiece()
	{
		this.pieceLockedValue = 2;
		this.spriteRenderer.sprite = doubleLockSprite;
	}

	public void UnlockPiece()
	{
		// if(this.pieceLockedValue == 2)
		// {
		// 	this.pieceLockedValue--;
		// 	this.spriteRenderer.sprite = singleLockSprite;
		// }
		// else if(this.pieceLockedValue == 1)
		// {
		this.pieceLockedValue = 0;
		this.spriteRenderer.sprite = unlockedSprite;
		// }
	}

	// Use this for initialization
	void Start () 
	{
		this.spriteRenderer = this.GetComponent<SpriteRenderer>();
		this.spriteRenderer.color = spriteColor;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
