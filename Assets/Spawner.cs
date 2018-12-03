using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public GameObject[] groups;

	public Color[] blockColors;

	private Group nextUp;
	public GameObject nextUpSlot;

	public void StartGame()
	{
		this.spawnNext();
		this.nextPiece();
	}

	public void nextPiece()
	{
		nextUp.transform.position = this.transform.position;
		nextUp.enablePiece();
		this.spawnNext();
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
