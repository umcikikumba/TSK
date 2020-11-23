using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Balls : MonoBehaviour
{
    public Slider bounce;
    public Slider static_friction;
    public Slider dynamic_friction;

    // Start is called before the first frame update
    void Start()
    {
        bounce.value = this.GetComponent<SphereCollider>().sharedMaterial.bounciness;
        static_friction.value = this.GetComponent<SphereCollider>().sharedMaterial.staticFriction;
        dynamic_friction.value = this.GetComponent<SphereCollider>().sharedMaterial.dynamicFriction;
    }

    // Update is called once per frame
    void Update()
    {
        
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
