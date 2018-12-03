using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public static SoundManager instance;

	public AudioSource placed;
	public AudioSource gameOver;
	public AudioSource clear;
	public AudioSource rotate;

	// Use this for initialization
	void Start () {
		
		SoundManager.instance = this;
	}

	public void PlayPlaced()
	{
		this.placed.Play();
	}

	public void PlayClear()
	{
		this.clear.Play();
	}

	public void PlayRotate()
	{
		this.rotate.Play();
	}

	public void PlayGameOver()
	{
		this.gameOver.Play();
	}
}
