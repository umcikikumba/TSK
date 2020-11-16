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
    public Transform startPosition;
    private bool isShooting;
    //private bool hasShot;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = this.transform;
        isShooting = false;
        whiteBall.transform.hasChanged = false;
        //startPosition.position = this.transform.position - whiteBall.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isShooting == false)
        {
            Vector3 v3Pos = Camera.main.WorldToScreenPoint(whiteBall.transform.position);
            v3Pos = Input.mousePosition - v3Pos;
            float angle = Mathf.Atan2(v3Pos.y, v3Pos.x) * Mathf.Rad2Deg;
            v3Pos = Quaternion.AngleAxis(angle, Vector3.up) * (Vector3.right * radius);
            transform.position = whiteBall.transform.position + v3Pos;
            Vector3 targetPostition = new Vector3(whiteBall.transform.position.x,
                                            this.transform.position.y,
                                            whiteBall.transform.position.z);
            this.transform.LookAt(targetPostition);
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
    }

    
}
