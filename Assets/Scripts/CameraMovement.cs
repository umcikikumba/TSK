using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Camera cam;
    public Transform target;
    public float movementSpeed;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(target);
        

        float xAxisValue = Input.GetAxis("Horizontal") * movementSpeed;
        float yAxisValue = Input.GetAxis("Vertical") * movementSpeed;
        float zAxisValue = 0.0f;
        if (Camera.current != null)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                zAxisValue -= movementSpeed;
            }
            else if (Input.GetKey(KeyCode.E))
            {
                zAxisValue += movementSpeed;
            }

            Camera.current.transform.Translate(new Vector3(xAxisValue, yAxisValue, zAxisValue));
        }
        
    }
}
