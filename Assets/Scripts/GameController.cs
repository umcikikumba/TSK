using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
    public GameObject cue;
    public GameObject cueBall;
    public GameObject[] Balls;
    public GameObject whiteBall;
    public GameObject[] fulls;
    public GameObject[] halfs;

    public Player Player1;
    public Player Player2;
    public Player currentPlayer;
    public Player otherPlayer;
    public bool currentPlayerContinuesToPlay = false;
    public bool ballInPocket = false;
    public bool firstBallPotted = false;
    public bool faul = false;
    public bool goodBall = false;
    public bool game_over = false;
    public bool win = false;
    public Text text;
    public Text reset;
    public Text gameOver;
    public Text winning;
    public Text currentBall;

    static public GameController GameInstance
    {
        get;
        private set;
    }
    void Start()
    {
        GameInstance = this;
        Player1 = new Player("Player 1");
        Player2 = new Player("Player 2");
        currentPlayer = Player1;
        otherPlayer = Player2;
        text.text = "Now playing " + currentPlayer.Name;
        CurrentBall();
        firstBallPotted = false;
    }

    private void Update()
    {
        text.text = "Now playing " + currentPlayer.Name;
        CurrentBall();
        if (faul)
        {
            if (Input.GetKey(KeyCode.Y))
            {
                whiteBall.transform.position = whiteBall.GetComponent<Balls>().startPos;
                whiteBall.GetComponent<Rigidbody>().velocity = Vector3.zero;
                whiteBall.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                cue.transform.position = cue.GetComponent<Cue>().startPos;
                cue.transform.rotation = cue.GetComponent<Cue>().startRot;
                reset.gameObject.SetActive(false);
                faul = false;
            }
            else if (Input.GetKey(KeyCode.N))
            {
                reset.gameObject.SetActive(false);
                faul = false;
            }
        }
        if (game_over)
        {
            gameOver.gameObject.SetActive(true);
            if (Input.GetKey(KeyCode.Escape))
            {
                
                whiteBall.transform.position = whiteBall.GetComponent<Balls>().startPos;
                whiteBall.GetComponent<Rigidbody>().velocity = Vector3.zero;
                whiteBall.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                cue.transform.position = cue.GetComponent<Cue>().startPos;
                cue.transform.rotation = cue.GetComponent<Cue>().startRot;
                foreach (GameObject it in Balls)
                {
                    it.gameObject.SetActive(true);
                    it.GetComponent<SphereCollider>().isTrigger = false;
                    it.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    it.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                    it.transform.position = it.GetComponent<Balls>().startPos;
                    
                }
                game_over = false;
                gameOver.gameObject.SetActive(false);
            }
        }
        if (win)
        {
            winning.gameObject.SetActive(true);
            if (Input.GetKey(KeyCode.Escape))
            {

                whiteBall.transform.position = whiteBall.GetComponent<Balls>().startPos;
                whiteBall.GetComponent<Rigidbody>().velocity = Vector3.zero;
                whiteBall.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                cue.transform.position = cue.GetComponent<Cue>().startPos;
                cue.transform.rotation = cue.GetComponent<Cue>().startRot;
                foreach (GameObject it in Balls)
                {
                    it.gameObject.SetActive(true);
                    it.GetComponent<SphereCollider>().isTrigger = false;
                    it.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    it.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                    it.transform.position = it.GetComponent<Balls>().startPos;

                }
                win = false;
                winning.gameObject.SetActive(false);
            }
        }

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
        if (!currentPlayerContinuesToPlay)
        {
            if (currentPlayer.Name == Player1.Name)
            {
                currentPlayer = Player2;
                otherPlayer = Player1;
            }
           else if(currentPlayer.Name == Player2.Name)
           {
                currentPlayer = Player1;
                otherPlayer = Player2;
           }
        }
    }
    
    public void faulWithReset()
    {
        faul = true;
        currentPlayerContinuesToPlay = false;
        NextPlayer();
        reset.gameObject.SetActive(true);
    }

    public void Lose()
    {
        game_over = true;
        faul = false;
        reset.gameObject.SetActive(false);
    }

    public void Win()
    {
        winning.gameObject.transform.position = gameOver.gameObject.transform.position;
        winning.gameObject.transform.localScale = gameOver.gameObject.transform.localScale;
        winning.text = currentPlayer.Name + " Wins!!! \nPress ESC to restart";
        win = true;
    }

    public void CurrentBall()
    {
        if(currentPlayer.currentPlayerHasFull == true)
        {
            int x = 0;
            for (int i = 0; i < 7; i++)
            {
                if (fulls[i].activeSelf == true)
                {
                    x = 1;
                }
            }
            if (x == 1)
            {
                currentBall.text = "Strike solid coloured balls 1-7";
            }
            else
            {
                currentBall.text = "Strike black ball 8";
            }
        }

        if(currentPlayer.currentPlayerHasHalf == true)
        {
            int x = 0;
            for (int i = 0; i < 7; i++)
            {
                if (fulls[i].activeSelf == true)
                {
                    x = 1;
                }
            }
            if (x == 1)
            {
                currentBall.text = "Strike striped balls 9 - 15";
            }
            else
            {
                currentBall.text = "Strike black ball 8";
            }
        }
    }


}
