using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShotPrediction : MonoBehaviour
{
    //TODO:
    //-> ogarniecie poprawnej sily aplikowanej do symulowanej bialej bili,
    //   cue.transform.forward musi byc dla kija blisko bili prawdopodobnie,
    //   jednoczesnie musi to byc z bomby a nie po wcisnieciu LPM

    private Scene sceneMain;
    private Scene scenePrediction;
    private PhysicsScene sceneMainPhysics;
    private PhysicsScene scenePredictionPhysics;

    public GameObject whiteBall;
    public GameObject cue;
    private int steps = 200;
    public GameObject table;

    private Vector3[] markerPos;
    private Material redMaterial;

    public GameObject ball;
    private GameObject[] ballsPredicted;
    public GameObject[] balls;
    private LineRenderer lineRenderer;

    public GameObject[] pockets;
    //private GameObject predictionCue;
    //private GameObject marker;

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
        Destroy(predictionTable.GetComponent<MeshRenderer>());
        SceneManager.MoveGameObjectToScene(predictionTable, scenePrediction);

        //instantiate pockets
        SetupPockets();

        /*//instantiate marker for testing new pos of transform forward in apply force
        marker = Instantiate(cue);
        marker.name = "cue_clone";
        Destroy(marker.GetComponent<BoxCollider>());
        marker.transform.position = cue.transform.position;
        marker.transform.rotation = cue.transform.rotation;
        SceneManager.MoveGameObjectToScene(marker, scenePrediction);
        */

        markerPos = new Vector3[steps];
        redMaterial = new Material(Shader.Find("Diffuse"));
        redMaterial.color = Color.red;
        
        ballsPredicted = new GameObject[15];
        for (int i = 0; i < 15; i++)
        {
            ballsPredicted[i] = Instantiate(ball);
            ballsPredicted[i].transform.position = balls[i].transform.position;
            Destroy(ballsPredicted[i].GetComponent<MeshRenderer>());
            SceneManager.MoveGameObjectToScene(ballsPredicted[i], scenePrediction);
            ballsPredicted[i].SetActive(true);
        }

        GetMarkersPos();
        lineRenderer = GetComponent<LineRenderer>();
        SetupLineRenderer(lineRenderer);
        UpdateBallsPositions();
        DrawLine();
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
        if(Input.GetKeyUp(KeyCode.A) || Input.GetKey(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKey(KeyCode.D) 
            || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKey(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow) 
                || Input.GetKey(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKey(KeyCode.LeftArrow) 
                    || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.R))
        {
            //UpdateBallsPositions();
            GetMarkersPos();
            DrawLine();
        }
        //DrawLine();
        //UpdateBallsPositions();
    }

    private void GetMarkersPos()
    {
        if (sceneMainPhysics.IsValid() == false || scenePredictionPhysics.IsValid() == false)
            return;

        UpdateBallsPositions();

        GameObject predictionBall = Instantiate(whiteBall);
        predictionBall.GetComponent<WhiteBall>().clone = true;
        SceneManager.MoveGameObjectToScene(predictionBall, scenePrediction);
        predictionBall.transform.position = whiteBall.transform.position;

        //cue.transform.forward nie wplywa, wektor nie zmienia sie przyblizajac do bialej bili
        //-> cos innego jest przyczyna, co?
        /*       
        GameObject marker = Instantiate(GameObject.CreatePrimitive(PrimitiveType.Sphere));
        marker.name = "siema";
        Destroy(marker.GetComponent<SphereCollider>());
        marker.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        
        marker.transform.position = cue.transform.position;
        marker.transform.rotation = cue.transform.rotation;
        float step = cue.GetComponent<Cue>().speedCueStrike * 0.7f;
        marker.transform.position = Vector3.Lerp(marker.transform.position, whiteBall.transform.position, step);
        */
        predictionBall.GetComponent<Rigidbody>().AddForceAtPosition(cue.transform.forward * cue.GetComponent<Cue>().force, 
            predictionBall.transform.position, ForceMode.Impulse);
        
        for (int i = 0; i < steps; i++)
        {
            scenePredictionPhysics.Simulate(Time.fixedDeltaTime);

            markerPos[i] = predictionBall.transform.position;
        }
        Destroy(predictionBall);

        UpdateBallsPositions();
    }

    private void UpdateBallsPositions()
    {
        for (int i = 0; i < 15; i++)
        {
            ballsPredicted[i].SetActive(true);
            ballsPredicted[i].transform.position = balls[i].transform.position;
            ballsPredicted[i].GetComponent<SphereCollider>().isTrigger = false;
        }
    }

    private void DrawLine()
    {
        lineRenderer.positionCount = markerPos.Length;
        lineRenderer.SetPositions(markerPos);
    }

    private void SetupLineRenderer(LineRenderer lineRenderer)
    {
        lineRenderer.startWidth = 0.03f;
        lineRenderer.endWidth = 0.03f;
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        lineRenderer.material = new Material(Shader.Find("Unlit/Color"));
        lineRenderer.sharedMaterial.color = Color.red;
    }

    private void SetupPockets()
    {
        GameObject pocket0 = Instantiate(pockets[0]);
        SceneManager.MoveGameObjectToScene(pocket0, scenePrediction);
        pocket0.transform.position = pockets[0].transform.position;
        GameObject pocket1 = Instantiate(pockets[1]);
        SceneManager.MoveGameObjectToScene(pocket1, scenePrediction);
        pocket1.transform.position = pockets[1].transform.position;
        GameObject pocket2 = Instantiate(pockets[2]);
        SceneManager.MoveGameObjectToScene(pocket2, scenePrediction);
        pocket2.transform.position = pockets[2].transform.position;
        GameObject pocket3 = Instantiate(pockets[3]);
        SceneManager.MoveGameObjectToScene(pocket3, scenePrediction);
        pocket3.transform.position = pockets[3].transform.position;
        GameObject pocket4 = Instantiate(pockets[4]);
        SceneManager.MoveGameObjectToScene(pocket4, scenePrediction);
        pocket4.transform.position = pockets[4].transform.position;
        GameObject pocket5 = Instantiate(pockets[5]);
        SceneManager.MoveGameObjectToScene(pocket5, scenePrediction);
        pocket5.transform.position = pockets[5].transform.position;
    }
    
}
