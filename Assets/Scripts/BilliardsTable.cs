using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BilliardsTable : MonoBehaviour
{
    public Slider bounce;
    public Slider static_friction;
    public Slider dynamic_friction;

    // Start is called before the first frame update
    void Start()
    {
        bounce.value = this.GetComponent<Collider>().sharedMaterial.bounciness;
        static_friction.value = this.GetComponent<Collider>().sharedMaterial.staticFriction;
        dynamic_friction.value = this.GetComponent<Collider>().sharedMaterial.dynamicFriction;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AdjustBounciness(float newBounciness)
    {
        this.GetComponent<BoxCollider>().sharedMaterial.bounciness = newBounciness;
        gameObject.GetComponent<BoxCollider>().enabled = false;
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }

    public void AdjustStaticFriction(float newStaticFriction)
    {
        this.GetComponent<BoxCollider>().sharedMaterial.staticFriction = newStaticFriction;
        gameObject.GetComponent<BoxCollider>().enabled = false;
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }

    public void AdjustDynamicFriction(float newDynamicFriction)
    {
        this.GetComponent<BoxCollider>().sharedMaterial.dynamicFriction = newDynamicFriction;
        gameObject.GetComponent<BoxCollider>().enabled = false;
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }
}
