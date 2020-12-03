using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShotPrediction : MonoBehaviour
{
    private Scene sceneMain;
    private Scene scenePrediction;
    private PhysicsScene sceneMainPhysics;
    private PhysicsScene scenePredictionPhysics;

    public GameObject whiteBall;
    public Cue cue;
    private int steps = 50;
    public GameObject table;

    private Vector3[] markerPos;
    private GameObject[] markers;
    private Material redMaterial;

    public GameObject ball;
    private GameObject[] ballsPredicted;
    public GameObject[] balls;
    
    // Start is called before the first frame update
    void Start()
    {
        //manual simulation
        Physics.autoSimulation = false;
        //get main scene
        sceneMain = SceneManager.GetActiveScene();
        sceneMainPhysics = sceneMain.GetPhysicsScene();

        //create physic scene
        CreateSceneParameters sceneParam = new CreateSceneParameters(LocalPhysicsMode.Physics3D);
        scenePrediction = SceneManager.CreateScene("scenePredictionPhysics", sceneParam);
        scenePredictionPhysics = scenePrediction.GetPhysicsScene();

        //instantiate table and move it to hidden physics scene
        GameObject predictionTable = Instantiate(table);
        SceneManager.MoveGameObjectToScene(predictionTable, scenePrediction);

        markerPos = new Vector3[steps];
        markers = new GameObject[steps];
        redMaterial = new Material(Shader.Find("Diffuse"));
        redMaterial.color = Color.red;
        
        for (int i = 0; i < steps; i++)
        {
            markers[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            markers[i].GetComponent<Collider>().isTrigger = true;
            markers[i].transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            markers[i].transform.position = Vector3.zero;
            markers[i].GetComponent<MeshRenderer>().material = redMaterial;
            SceneManager.MoveGameObjectToScene(markers[i], scenePrediction);
        }

        ballsPredicted = new GameObject[15];
        for (int i = 0; i < 15; i++)
        {
            ballsPredicted[i] = Instantiate(ball);
            ballsPredicted[i].transform.position = balls[i].transform.position;
            Destroy(ballsPredicted[i].GetComponent<MeshRenderer>());
            SceneManager.MoveGameObjectToScene(ballsPredicted[i], scenePrediction);
        }        
    }

    void FixedUpdate()
    {
        if(sceneMainPhysics.IsValid() == false)
            return;

        sceneMainPhysics.Simulate(Time.fixedDeltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) ||
            Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            DrawLine();
            UpdateBallsPositions();
        }
    }

    private void DrawLine()
    {
        if (sceneMainPhysics.IsValid() == false || scenePredictionPhysics.IsValid() == false)
            return;

        GameObject predictionBall = Instantiate(whiteBall);
        SceneManager.MoveGameObjectToScene(predictionBall, scenePrediction);
        predictionBall.transform.position = whiteBall.transform.position;
        predictionBall.GetComponent<Rigidbody>().velocity = Vector3.zero;
        predictionBall.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        predictionBall.GetComponent<Rigidbody>().AddTorque(Vector3.forward * cue.force * 10);
        predictionBall.GetComponent<Rigidbody>().AddForceAtPosition(cue.transform.forward * cue.force, 
            predictionBall.transform.position, ForceMode.Impulse);

        Material redMaterial = new Material(Shader.Find("Diffuse"));
        redMaterial.color = Color.red;
        
        for (int i = 0; i < steps; i++)
        {
            scenePredictionPhysics.Simulate(Time.fixedDeltaTime);

            markerPos[i] = predictionBall.transform.position;
            markers[i].transform.position = markerPos[i];
        }
        Destroy(predictionBall);
    }

    private void UpdateBallsPositions()
    {
        for (int i = 0; i < 15; i++)
        {
            ballsPredicted[i].transform.position = balls[i].transform.position;
        }
    }
}
