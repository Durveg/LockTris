using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour 
{
	public SpriteRenderer spriteRenderer;
	public bool pieceLocked = false;

	public Color spriteColor;

	public Sprite unlockedSprite;
	public Sprite lockedSprite;

	public void SetLocked()
	{
		pieceLocked = true;
	}

	public void LockPiece()
	{
		this.spriteRenderer.sprite = lockedSprite;
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
