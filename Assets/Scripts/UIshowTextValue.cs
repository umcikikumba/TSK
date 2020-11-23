using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIshowTextValue : MonoBehaviour
{
    Text text;
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        showBouncinessValue();    
    }

    public void showBouncinessValue()
    {
        text.text = slider.value.ToString();
    }
}
