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
        // Gets a vector that points from the player's position to the target's.
        //var heading = this.transform.position - cue.transform.position;
        //var distance = heading.magnitude;
        //var direction = heading / distance; // This is now the normalized direction.


        //rb.AddTorque(Vector3.forward * cue.force * 10);
        //rb.velocity.Set(cue.force / rb.mass, cue.force / rb.mass, cue.force / rb.mass);
        //rb.AddForceAtPosition(cue.transform.forward * cue.force,this.transform.position, ForceMode.Impulse);
        //rb.AddForceAtPosition(cue.transform.forward * cue.force, new Vector3(10.0f, 0.0f, 0.0f), ForceMode.Impulse);
        //rb.AddForceAtPosition(cue.transform.forward *  cue.force, new Vector3(10.0f, 0.0f, 0.0f), ForceMode.Impulse);

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
            if(collision.gameObject.name == "9")
            {
                Debug.Log("uderzyles w 9 bile");

            }
        }
    }

    
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Pocket"))
        {
            if(clone == false)
            {
                //Debug.Log("White ball has entered the pocket.");
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                collidedWithPocket = true;
                this.transform.position = startPosition;
            }
            else if (clone == true)
            {
                //stop drawing motherfucker
                //Debug.Log("Clone ball has entered the pocket.");
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                collidedWithPocket = true;
                Vector3 newStartPos = startPosition;
                newStartPos.y -= 1.0f;
                this.transform.position = newStartPos;
                //this.transform.position = startPosition;
            }
        }
    }
}
