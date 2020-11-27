using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cue : MonoBehaviour
{
    public float force ;
    public float speedCueStrike = 1.0f;
    public float radius = 2.0f;
    public WhiteBall whiteBall;
    public Vector3 startPos;
    public Quaternion startRot;
    private bool isShooting;
    public float rotateStep = 0.0f;
    public Camera cam;
    public float speedRotation = 30.0f;

    // Start is called before the first frame update
    void Start()
    {
        //startPosition = this.transform;
        isShooting = false;
        whiteBall.transform.hasChanged = false;
        startPos = this.transform.position;
        startRot = this.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (isShooting == false)
        {
            Vector3 targetPos = new Vector3(whiteBall.transform.position.x, whiteBall.transform.position.y + 0.2f, whiteBall.transform.position.z);
            if (Input.GetKey(KeyCode.A))
            {
                transform.RotateAround(targetPos, Vector3.up, -speedRotation * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.RotateAround(targetPos, Vector3.up, speedRotation * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                if (this.transform.rotation.x > 0.007f && this.transform.rotation.x < 0.042f)
                {
                    rotateStep = -5.0f * Time.deltaTime;
                    transform.Rotate(rotateStep, 0.0f, 0.0f);
                }
                else
                {
                    rotateStep = 0.0f;
                }
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                if (this.transform.rotation.x > 0.005f && this.transform.rotation.x < 0.039f)
                {
                    rotateStep = 5.0f * Time.deltaTime;
                    transform.Rotate(rotateStep, 0.0f, 0.0f);
                }
                else
                {
                    rotateStep = 0.0f;
                }
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (this.transform.rotation.y > -0.73f && this.transform.rotation.y < -0.68f)
                {
                    rotateStep = -5.0f * Time.deltaTime;
                    transform.Rotate(0.0f, rotateStep, 0.0f);
                }
                else
                {
                    rotateStep = 0.0f;
                }
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                if (this.transform.rotation.y > -0.74f && this.transform.rotation.y < -0.69f)
                {
                    rotateStep = 5.0f * Time.deltaTime;
                    transform.Rotate(0.0f, rotateStep, 0.0f);
                }
                else
                {
                    rotateStep = 0.0f;
                }
            }
        }

        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            float step = speedCueStrike * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, whiteBall.transform.position, step);
            isShooting = true;
        }

        if (whiteBall.velocity < 0.1f && (whiteBall.transform.hasChanged == true || whiteBall.collidedWithPocket == true))
        {
            isShooting = false;
            whiteBall.collidedWithPocket = false;
            whiteBall.transform.hasChanged = false;
            UpdateCuePosition();
        }

        if (Input.GetKey(KeyCode.R))
        {
            transform.position = startPos;
            transform.rotation = startRot;
        }
    }

    public void adjustForce(float newForce)
        {
            force = newForce;
        }

    void OnCollisionEnter (Collision collision)
    {
        if (collision.gameObject.tag == "white_ball")         //"Balls" layer
        {
            collision.gameObject.SendMessage("ApplyForce", this);
        }
        if (collision.gameObject.tag == "balls")
        {
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
        }
    }    

    public void UpdateCuePosition()
    {
        //newpos
        Vector3 newWhiteBallPos = new Vector3(whiteBall.transform.position.x + 2.0f, whiteBall.transform.position.y + 0.2f, whiteBall.transform.position.z);
        this.transform.position = newWhiteBallPos;
        transform.Rotate(5.0f, 0.0f, 0.0f);

        //lookat
        this.transform.LookAt(whiteBall.transform);
    }
}
