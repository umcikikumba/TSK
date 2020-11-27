using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Camera cam;
    public Transform target;
    public Transform cue;
    public float movementSpeed;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newTarget = new Vector3(target.position.x - 3.0f, target.position.y + 0.3f, target.position.z);
        transform.LookAt(newTarget);
        /*
        float xAxisValue = 0.0f;
        float yAxisValue = 0.0f;
        float zAxisValue = 0.0f;
        if (Camera.current != null)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                xAxisValue -= movementSpeed;
                Debug.Log("up");
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                xAxisValue += movementSpeed;
                Debug.Log("down");
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                yAxisValue -= movementSpeed;
                Debug.Log("left");
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                yAxisValue += movementSpeed;
                Debug.Log("r");
            }

            if (Input.GetKey(KeyCode.Q))
            {
               zAxisValue -= movementSpeed;
                Debug.Log("zoomup");
            }
            else if (Input.GetKey(KeyCode.E))
            {
                zAxisValue += movementSpeed;
                Debug.Log("zoomdown");
            }
       
            Camera.current.transform.Translate(new Vector3(xAxisValue, yAxisValue, zAxisValue));
        }
        */
        Vector3 newPos = new Vector3(cue.transform.position.x + 1.5f, cue.transform.position.y + 0.2f, cue.transform.position.z);
        this.transform.position = newPos;
    }
}
