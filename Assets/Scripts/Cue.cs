using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cue : MonoBehaviour
{
    public float force ;
    public float speed = 1.0f;
    public float radius = 2.0f;
    public WhiteBall whiteBall;
    public Vector3 startPos;
    public Quaternion startRot;
    private bool isShooting;
    //private bool hasShot;
    float horizontal = 0.0f;
    float vertical = 0.0f;

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
                Debug.Log("siemaA");
                transform.RotateAround(targetPos, Vector3.up, -10.0f * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                Debug.Log("siemaD");
                transform.RotateAround(targetPos, Vector3.up, 10.0f * Time.deltaTime);
            }

            //vertical_x -> 2.9-5.7
            if (Input.GetKey(KeyCode.UpArrow))
            {
                Debug.Log("U");
                vertical -= 1.0f * Time.deltaTime;
                transform.Rotate(vertical, 0.0f, 0.0f);  
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                Debug.Log("D");
                vertical += 1.0f * Time.deltaTime;
                transform.Rotate(vertical, 0.0f, 0.0f);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                Debug.Log("L");
                horizontal += 1.0f * Time.deltaTime;
                transform.Rotate(0.0f, horizontal, 0.0f);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                Debug.Log("R");
                horizontal -= 1.0f * Time.deltaTime;
                transform.Rotate(0.0f, horizontal, 0.0f);
            }

        }

        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, whiteBall.transform.position, step);
            isShooting = true;
        }

        if (whiteBall.velocity < 0.1f && (whiteBall.transform.hasChanged == true || whiteBall.collidedWithPocket == true))
        {
            isShooting = false;
            whiteBall.collidedWithPocket = false;
            whiteBall.transform.hasChanged = false;
        }

        if (Input.GetKey(KeyCode.R))
        {
            transform.position = startPos;
            transform.rotation = startRot;
            Debug.Log("resetuje kija");
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

    }
}
