using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socket : MonoBehaviour
{
    public GameObject ball;
    public GameController gameController;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        var player1 = GameController.GameInstance.Player1;
        var player2 = GameController.GameInstance.Player2;
        var currentPlayer = GameController.GameInstance.currentPlayer;
        var otherPlayer = GameController.GameInstance.otherPlayer;
        if (!gameController.firstBallPotted)
        {
            if (collision.gameObject.CompareTag("Pocket"))
            {
                
                if(this.name == "1" || this.name == "2" || this.name == "3" || this.name == "4" || this.name == "5" || this.name == "6" || this.name == "7")
                {
                    currentPlayer.currentPlayerHasFull = true;
                    otherPlayer.currentPlayerHasHalf = true;
                    ball.SetActive(false);
                    ball.GetComponent<Collider>().isTrigger = true;
                    gameController.ballInPocket = true;
                    gameController.firstBallPotted = true;
                    gameController.goodBall = true;

                }
                if (this.name == "9" || this.name == "10" || this.name == "11" || this.name == "12" || this.name == "13" || this.name == "14" || this.name == "15")
                {
                    currentPlayer.currentPlayerHasHalf = true;
                    otherPlayer.currentPlayerHasFull = true;
                    ball.SetActive(false);
                    ball.GetComponent<Collider>().isTrigger = true;
                    gameController.ballInPocket = true;
                    gameController.firstBallPotted = true;
                    gameController.goodBall = true;
                }
                if (this.name == "8")
                {
                    gameController.Lose();
                }
            }
        }
        else if (gameController.firstBallPotted)
        {
            if (collision.gameObject.CompareTag("Pocket"))
            {
                
                if (gameController.currentPlayer.currentPlayerHasHalf)
                {
                    if(this.name == "9" || this.name == "10" || this.name == "11" || this.name == "12" || this.name == "13" || this.name == "14" || this.name == "15")
                    {
                        gameController.goodBall = true;
                        ball.SetActive(false);
                        ball.GetComponent<Collider>().isTrigger = true;
                        gameController.ballInPocket = true;

                    }
                    if (this.name == "1" || this.name == "2" || this.name == "3" || this.name == "4" || this.name == "5" || this.name == "6" || this.name == "7")
                    {
                        ball.SetActive(false);
                        ball.GetComponent<Collider>().isTrigger = true;
                        if (!gameController.goodBall)
                        {
                            gameController.faulWithReset();

                        }

                    }
                    if(this.name == "8")
                    {
                        int x = 0;
                        for(int i = 0; i < 7; i++)
                        {
                            if(gameController.halfs[i].activeSelf == true)
                            {
                                gameController.Lose();
                                x = 1;
                            }
                        }
                        if (x == 0)
                        {
                            gameController.Win();
                        }
                    }
                }
                if (gameController.currentPlayer.currentPlayerHasFull)
                {
                    if (this.name == "1" || this.name == "2" || this.name == "3" || this.name == "4" || this.name == "5" || this.name == "6" || this.name == "7")
                    {
                        gameController.goodBall = true;
                        ball.SetActive(false);
                        ball.GetComponent<Collider>().isTrigger = true;
                        gameController.ballInPocket = true;
                    }
                    if (this.name == "9" || this.name == "10" || this.name == "11" || this.name == "12" || this.name == "13" || this.name == "14" || this.name == "15")
                    {
                        ball.SetActive(false);
                        ball.GetComponent<Collider>().isTrigger = true;
                        if (!gameController.goodBall)
                        {
                            gameController.faulWithReset();

                        }
                    }
                    if (this.name == "8")
                    {
                        int x = 0;
                        for (int i = 0; i < 7; i++)
                        {
                            if (gameController.fulls[i].activeSelf == true)
                            {
                                gameController.Lose();
                                x = 1;
                            }
                        }
                        if(x == 0)
                        {
                            gameController.Win();
                        }
                    }
                }    
            }

        }
    }
}
