using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIsetSliderValue : MonoBehaviour
{
    Slider slider;
    public GameObject gameObject;
    public bool bounciness;
    public bool staticFriction;
    public bool dynamicFriction;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        if (bounciness == true)
        {
            slider.value = gameObject.GetComponent<Collider>().sharedMaterial.bounciness;
        }
        else if (staticFriction == true)
        {
            slider.value = gameObject.GetComponent<Collider>().sharedMaterial.staticFriction;
        }
        else if (dynamicFriction == true)
        {
            slider.value = gameObject.GetComponent<Collider>().sharedMaterial.dynamicFriction;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
