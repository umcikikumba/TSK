using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BilliardsTable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AdjustBounciness(float newBounciness)
    {
        this.GetComponent<Collider>().sharedMaterial.bounciness = newBounciness;
        gameObject.GetComponent<BoxCollider>().enabled = false;
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }

    public void AdjustStaticFriction(float newStaticFriction)
    {
        this.GetComponent<Collider>().sharedMaterial.staticFriction = newStaticFriction;
        gameObject.GetComponent<BoxCollider>().enabled = false;
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }

    public void AdjustDynamicFriction(float newDynamicFriction)
    {
        this.GetComponent<Collider>().sharedMaterial.dynamicFriction = newDynamicFriction;
        gameObject.GetComponent<BoxCollider>().enabled = false;
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }
}
