using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public bool currentPlayerHasFull = false;
	public bool currentPlayerHasHalf = false;

	public Player(string name)
	{
		Name = name;
	}

	public string Name
	{
		get;
		private set;
	}

	
}
