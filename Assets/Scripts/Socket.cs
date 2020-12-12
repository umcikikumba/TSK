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
        if (collision.gameObject.CompareTag("Pocket"))
        {
            ball.SetActive(false);
            ball.GetComponent<Collider>().isTrigger = true;
            gameController.ballInPocket = true;
            //Object.Destroy(ball);
        }
    }
}
