using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //private Camera cam;
    public Transform whiteBall;
    public Transform cue;
    public Transform cameraPosOnCue;
    public float movementSpeed;
    public Camera cueCamera;
    public Camera topCamera;
    // 0->cueCamera; 1->topCamera //
    private int cameraChoice; 

    // Start is called before the first frame update
    void Start()
    {
        //cam = Camera.main;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //if ()
        cueCamera.transform.position = cameraPosOnCue.transform.position;
        cueCamera.transform.LookAt(whiteBall.transform.position);

        if (Input.GetKeyDown(KeyCode.P))
        {
            ShowTopView();
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            ShowCueView();
        }
    }

    // Call this function to disable FPS camera,
    // and enable overhead camera.
    public void ShowTopView()
    {
        cueCamera.enabled = false;
        topCamera.enabled = true;
    }

    // Call this function to enable FPS camera,
    // and disable overhead camera.
    public void ShowCueView()
    {
        cueCamera.enabled = true;
        topCamera.enabled = false;
    }
}
