using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject cue;
    public GameObject cueBall;
    public GameObject Balls;
    public GameObject scoreBar;
    public GameObject winnerMessage;

    public Player CurrentPlayer;
    public Player OtherPlayer;
    private bool currentPlayerContinuesToPlay = false;

    static public GameController GameInstance
    {
        get;
        private set;
    }

    void Start()
    {
        CurrentPlayer = new Player("Piotrek");
        OtherPlayer = new Player("Paweł");

        GameInstance = this;
               
    }
	public void BallPocketed(int ballNumber)
	{
		currentPlayerContinuesToPlay = true;
		CurrentPlayer.Collect(ballNumber);
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

	public void EndMatch()
	{
		Player winner = null;
		if (CurrentPlayer.Points > OtherPlayer.Points)
			winner = CurrentPlayer;
		else if (CurrentPlayer.Points < OtherPlayer.Points)
			winner = OtherPlayer;

		var msg = "Game Over\n";

		if (winner != null)
			msg += string.Format("The winner is '{0}'", winner.Name);
		else
			msg += "It was a draw!";

		var text = winnerMessage.GetComponentInChildren<UnityEngine.UI.Text>();
		text.text = msg;
		winnerMessage.GetComponent<Canvas>().enabled = true;
	}
}
