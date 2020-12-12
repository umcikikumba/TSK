using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject cue;
    public GameObject cueBall;
    public GameObject Balls;
    public GameObject[] fulls;
    public GameObject[] halfs;

    public Player CurrentPlayer;
    public Player OtherPlayer;
    public bool currentPlayerContinuesToPlay = false;
    public bool ballInPocket = false;
	public Text text;


    void Start()
    {
        CurrentPlayer = new Player("Piotrek");
        OtherPlayer = new Player("Paweł");
        text.text = "Now Playing " + CurrentPlayer.Name;
    }

    public void updatePlayer()
    {
        if (ballInPocket)
        {
            currentPlayerContinuesToPlay = true;
        }
    }

    public void NextPlayer()
	{
        updatePlayer();
        if (currentPlayerContinuesToPlay)
        {
            currentPlayerContinuesToPlay = false;
            return;
        }

        var aux = CurrentPlayer;
        CurrentPlayer = OtherPlayer;
        OtherPlayer = aux;
        text.text = "Now Playing " + CurrentPlayer.Name;

    }
      
}
