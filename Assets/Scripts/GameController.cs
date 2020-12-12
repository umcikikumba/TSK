using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject cue;
    public GameObject cueBall;
    public GameObject Balls;

    public Player CurrentPlayer;
    public Player OtherPlayer;
    private bool currentPlayerContinuesToPlay = false;
	public Text text;


    void Start()
    {
        CurrentPlayer = new Player("Piotrek");
        OtherPlayer = new Player("Paweł");
               
    }

    private void Update()
    {
		text.text = "Now Playing " +  CurrentPlayer.Name;
    }


	public void NextPlayer()
	{
		if (currentPlayerContinuesToPlay)
		{
			currentPlayerContinuesToPlay = false;
			Debug.Log(CurrentPlayer.Name + " continues to play");
			return;
		}

		Debug.Log(OtherPlayer.Name + " will play");
		var aux = CurrentPlayer;
		CurrentPlayer = OtherPlayer;
		OtherPlayer = aux;
	}

}
