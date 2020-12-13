using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhiteBall : MonoBehaviour
{
    public Rigidbody rb;
    public float velocity;
    private Vector3 startPosition;
    public bool collidedWithPocket, firstCollison;
    public Cue cue;
    public bool clone = false;
    public bool reachedVel = false;
    public GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = this.transform.position;
        collidedWithPocket = false;
        firstCollison = false;
    }

    // Update is called once per frame
    void Update()
    {
        velocity = rb.velocity.magnitude;

        if (rb.velocity.magnitude > 1.0f)
            reachedVel = true;
    }


    void ApplyForce (Cue cue)
    {
        rb.AddForce(cue.transform.forward * cue.force, ForceMode.Impulse);
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("balls"))
        {
            if (!firstCollison)
            {
                if (cue.reverse)
                {
                    rb.AddForceAtPosition(-cue.transform.forward * 1, this.transform.position, ForceMode.Impulse);
                    firstCollison = true;
                }
                if (cue.front)
                {
                    rb.AddForceAtPosition(cue.transform.forward * 1, this.transform.position, ForceMode.Impulse);
                    firstCollison = true;
                }
            }
            if (gameController.currentPlayer.currentPlayerHasHalf)
            {
                if (collision.gameObject.name == "1" || collision.gameObject.name == "2" || collision.gameObject.name == "3" || collision.gameObject.name == "4" || collision.gameObject.name == "5" || collision.gameObject.name == "6" || collision.gameObject.name == "7")
                {
                    gameController.goodTouch = true;
                }
                if (collision.gameObject.name == "1" || collision.gameObject.name == "2" || collision.gameObject.name == "3" || collision.gameObject.name == "4" || collision.gameObject.name == "5" || collision.gameObject.name == "6" || collision.gameObject.name == "7")
                {
                    if (!gameController.goodTouch)
                    {
                        gameController.faulWithReset();
                        Debug.Log("faul");
                    } 
                }
                if (collision.gameObject.name == "8")
                {
                    if (!gameController.goodTouch)
                    {
                        for (int i = 0; i < 7; i++)
                        {
                            if (gameController.halfs[i].activeSelf == true)
                            {
                                gameController.faulWithReset();
                            }
                        }
                    }
                }
            }
            if (gameController.currentPlayer.currentPlayerHasFull)
            {
                if (collision.gameObject.name == "1" || collision.gameObject.name == "2" || collision.gameObject.name == "3" || collision.gameObject.name == "4" || collision.gameObject.name == "5" || collision.gameObject.name == "6" || collision.gameObject.name == "7")
                {
                    gameController.goodTouch = true;
                }
                if (collision.gameObject.name == "9" || collision.gameObject.name == "10" || collision.gameObject.name == "11" || collision.gameObject.name == "12" || collision.gameObject.name == "13" || collision.gameObject.name == "14" || collision.gameObject.name == "15")
                {
                    if (!gameController.goodTouch)
                    {
                        gameController.faulWithReset();
                        Debug.Log("faul");
                    }
                }
                if(collision.gameObject.name == "8")
                {
                    if (!gameController.goodTouch)
                    {
                        for (int i = 0; i < 7; i++)
                        {
                            if (gameController.fulls[i].activeSelf == true)
                            {
                                gameController.faulWithReset();

                            }
                        }
                    }
                }
            }
        }
    }

    
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Pocket"))
        {
            if(clone == false)
            {
                
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                collidedWithPocket = true;
                this.transform.position = startPosition;
            }
            else if (clone == true)
            {

                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                collidedWithPocket = true;
                Vector3 newStartPos = startPosition;
                newStartPos.y -= 1.0f;
                this.transform.position = newStartPos;

            }
        }
    }
}
