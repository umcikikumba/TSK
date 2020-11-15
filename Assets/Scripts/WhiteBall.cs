using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteBall : MonoBehaviour
{
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ApplyForce (Cue cue)
    {
        // Gets a vector that points from the player's position to the target's.
        var heading = this.transform.position - cue.transform.position;
        var distance = heading.magnitude;
        var direction = heading / distance; // This is now the normalized direction.

        rb.AddForce(direction *  cue.force, ForceMode.Acceleration);
    }
}
