using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	private IList<int> ballsCollected = new List<int>();

	public Player(string name)
	{
		Name = name;
	}

	public string Name
	{
		get;
		private set;
	}

	public int Points
	{
		get { return ballsCollected.Count; }
	}

	public void Collect(int ballNumber)
	{
		Debug.Log(Name + " collected ball " + ballNumber);
		ballsCollected.Add(ballNumber);
	}
}
