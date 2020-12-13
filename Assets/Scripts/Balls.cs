using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Balls : MonoBehaviour
{
    public Slider bounce;
    public Slider static_friction;
    public Slider dynamic_friction;
    public Vector3 startPos;
    //public bool clone = false;

    // Start is called before the first frame update
    void Start()
    {
        bounce.value = this.GetComponent<SphereCollider>().sharedMaterial.bounciness;
        static_friction.value = this.GetComponent<SphereCollider>().sharedMaterial.staticFriction;
        dynamic_friction.value = this.GetComponent<SphereCollider>().sharedMaterial.dynamicFriction;
        startPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            gameObject.transform.position = startPos;
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
        
    }

    public void AdjustBounciness(float newBounciness)
    {
        this.GetComponent<SphereCollider>().sharedMaterial.bounciness = newBounciness;
        gameObject.GetComponent<SphereCollider>().enabled = false;
        gameObject.GetComponent<SphereCollider>().enabled = true;
    }

    public void AdjustStaticFriction(float newStaticFriction)
    {
        this.GetComponent<SphereCollider>().sharedMaterial.staticFriction = newStaticFriction;
        gameObject.GetComponent<SphereCollider>().enabled = false;
        gameObject.GetComponent<SphereCollider>().enabled = true;
    }

    public void AdjustDynamicFriction(float newDynamicFriction)
    {
        this.GetComponent<SphereCollider>().sharedMaterial.dynamicFriction = newDynamicFriction;
        gameObject.GetComponent<SphereCollider>().enabled = false;
        gameObject.GetComponent<SphereCollider>().enabled = true;
    }
}
