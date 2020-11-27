using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Camera cam;
    public Transform whiteBall;
    public Transform cue;
    public Transform cameraPosOnCue;
    public float movementSpeed;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        this.transform.position = cameraPosOnCue.transform.position;
        transform.LookAt(whiteBall.transform.position);
    }
}
