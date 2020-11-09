using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cue : MonoBehaviour
{
    public float force = 1.0f;
    public float speed = 1.0f;
    public Transform whiteBall;
    public Transform startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, whiteBall.position, step);
        }
    }

    void OnCollisionEnter (Collision collision)
    {
        if (collision.gameObject.tag == "white_ball")         //"Balls" layer
        {
            collision.gameObject.SendMessage("ApplyForce", this);
        }
    }
}
