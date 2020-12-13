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
    public bool reverse, front;
    public float rotateStep = 0.0f;
    public Camera cam;
    public float speedRotation = 30.0f;
    private float u1;
    private float u2;
    private float d1;
    private float d2;
    private float l1;
    private float l2;
    private float r1;
    private float r2;
    public BoxCollider boxCollider;
    private GameObject[] balls;
    private bool collidedWithBall;
    public GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        isShooting = false;
        reverse = false;
        front = false;
        whiteBall.transform.hasChanged = false;
        startPos = this.transform.position;
        startRot = this.transform.rotation;
        u1 = 0.0f;
        u2 = 0.0f;
        d1 = 0.0f;
        d2 = 0.0f;
        l1 = 0.0f;
        l2 = 0.0f;
        r1 = 0.0f;
        r2 = 0.0f;

        balls = GameObject.FindGameObjectsWithTag("balls");
        collidedWithBall = false;
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
                UpdateCueRotation();
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.RotateAround(targetPos, Vector3.up, speedRotation * Time.deltaTime);
                UpdateCueRotation();
            }

            else if (Input.GetKey(KeyCode.UpArrow))
            {
                if (this.transform.rotation.x > u1 && this.transform.rotation.x < u2)
                {
                    rotateStep = -5.0f * Time.deltaTime;
                    transform.Rotate(rotateStep, 0.0f, 0.0f);
                    front = true;
                    reverse = false;
                }
                else
                {
                    rotateStep = 0.0f;
                }
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                if (this.transform.rotation.x > d1 && this.transform.rotation.x < d2)
                {
                    rotateStep = 5.0f * Time.deltaTime;
                    transform.Rotate(rotateStep, 0.0f, 0.0f);
                    reverse = true;
                    front = false;
                }
                else
                {
                    rotateStep = 0.0f;
                }
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (this.transform.rotation.y > l1 && this.transform.rotation.y < l2)
                {
                    rotateStep = -5.0f * Time.deltaTime;
                    transform.Rotate(0.0f, rotateStep, 0.0f);
                }
                else
                {
                    rotateStep = 0.0f;
                }
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                if (this.transform.rotation.y > r1 && this.transform.rotation.y < r2)
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
            transform.position = Vector3.Lerp(transform.position, whiteBall.transform.position, step);
            isShooting = true;
        }

        if (collidedWithBall == true && whiteBall.velocity < 0.01f && whiteBall.reachedVel == true && 
            (whiteBall.transform.hasChanged == true || whiteBall.collidedWithPocket == true))
        {
            isShooting = false;
            whiteBall.collidedWithPocket = false;
            whiteBall.transform.hasChanged = false;
            //whiteBall.firstCollison = false;
            UpdateCuePosition();
            UpdateCueRotation();
            collidedWithBall = false;
            whiteBall.reachedVel = false;
            boxCollider.isTrigger = false;
        }


        if (Input.GetKey(KeyCode.R))
        {
            transform.position = startPos;
            transform.rotation = startRot;

            for (int i = 0; i < 15; i++)
            {
                balls[i].SetActive(true);
                balls[i].GetComponent<Collider>().isTrigger = false;
            }
        
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
            boxCollider.isTrigger = true;
            collidedWithBall = true;
            gameController.ballInPocket = false;
        }
        if (collision.gameObject.tag == "balls")
        {
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
        }
    }    

    public void UpdateCuePosition()
    {
        //newpos
        Vector3 newWhiteBallPos = new Vector3(whiteBall.transform.position.x + 1.6f, whiteBall.transform.position.y + 0.2f, whiteBall.transform.position.z);
        this.transform.position = newWhiteBallPos;
        transform.Rotate(5.0f, 0.0f, 0.0f);
        whiteBall.firstCollison = false;
        //lookat
        this.transform.LookAt(whiteBall.transform);
        gameController.NextPlayer();
        gameController.counter = 0;
        gameController.goodBall = false;
    }

    public void UpdateCueRotation()
    {
        u1 = this.transform.rotation.x - 0.02f;
        u2 = this.transform.rotation.x + 0.02f;
        d1 = this.transform.rotation.x - 0.022f;
        d2 = this.transform.rotation.x + 0.018f;
        l1 = this.transform.rotation.y - 0.022f;
        l2 = this.transform.rotation.y + 0.022f;
        r1 = this.transform.rotation.y - 0.024f;
        r2 = this.transform.rotation.y + 0.020f;
    }
}
