using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
	public static GameManager instance;

	public float fallSpeed = 1;

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
}
