using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhiteBall : MonoBehaviour
{
    public Rigidbody rb;
    public float velocity;
    private Vector3 startPosition;
    public bool collidedWithPocket;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = this.transform.position;
        collidedWithPocket = false;
    }

    // Update is called once per frame
    void Update()
    {
        velocity = rb.velocity.magnitude;
    }

    void ApplyForce (Cue cue)
    {
        // Gets a vector that points from the player's position to the target's.
        var heading = this.transform.position - cue.transform.position;
        var distance = heading.magnitude;
        var direction = heading / distance; // This is now the normalized direction.


        //rb.AddTorque(Vector3.down * cue.force);
        rb.AddForceAtPosition(direction * cue.force, new Vector3(10.0f, 0.0f, 0.0f), ForceMode.Impulse);
        //rb.AddForceAtPosition(cue.transform.forward *  cue.force, new Vector3(10.0f, 0.0f, 0.0f), ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Pocket"))
        {
            Debug.Log("White ball has entered the pocket.");
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            collidedWithPocket = true;
            this.transform.position = startPosition;
        }
    }

    
}
