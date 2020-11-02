using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cue : MonoBehaviour
{
    public float force = 10.0f;
    public float speed = 1.0f;
    public Transform whiteBall;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, whiteBall.position, step);
        }
    }

    void OnCollisionEnter (Collision collision)
    {
        if(collision.gameObject.layer == 8)         //"Balls" layer
        {
            collision.gameObject.SendMessage("ApplyForce", this);
        }
    }
}
