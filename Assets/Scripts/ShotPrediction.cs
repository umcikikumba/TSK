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
    //-> przerwa w rysowaniu linerenderera, gdy wpada do pocketu,
    //   jakos trzeba zmienic polozenie i-tych el. markerPos, ktore sa po kolizji z pocketem 

    private Scene sceneMain;
    private Scene scenePrediction;
    private PhysicsScene sceneMainPhysics;
    private PhysicsScene scenePredictionPhysics;

    public GameObject whiteBall;
    public Cue cue;
    private int steps = 200;
    public GameObject table;

    private Vector3[] markerPos;
    private Material redMaterial;

    public GameObject ball;
    private GameObject[] ballsPredicted;
    public Transform[] ballsPos;
    private LineRenderer lineRenderer;

    public GameObject[] pockets;

    //private GameObject predictionBall;

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

        markerPos = new Vector3[steps];
        redMaterial = new Material(Shader.Find("Diffuse"));
        redMaterial.color = Color.red;
        
        ballsPredicted = new GameObject[15];
        for (int i = 0; i < 15; i++)
        {
            ballsPredicted[i] = Instantiate(ball);
            ballsPredicted[i].transform.position = ballsPos[i].position;
            Destroy(ballsPredicted[i].GetComponent<MeshRenderer>());
            SceneManager.MoveGameObjectToScene(ballsPredicted[i], scenePrediction);
        }

        GetMarkersPos();
        lineRenderer = GetComponent<LineRenderer>();
        SetupLineRenderer(lineRenderer);
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
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) ||
            Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetMouseButton(0))
        {
            GetMarkersPos();
            DrawLine();
        }
        //DrawLine();
        UpdateBallsPositions();
    }

    private void GetMarkersPos()
    {
        if (sceneMainPhysics.IsValid() == false || scenePredictionPhysics.IsValid() == false)
            return;

        GameObject predictionBall = Instantiate(whiteBall);
        predictionBall.GetComponent<WhiteBall>().clone = true;
        SceneManager.MoveGameObjectToScene(predictionBall, scenePrediction);
        predictionBall.transform.position = whiteBall.transform.position;

        //cue.transform.forward sie zmienia wraz z przyblizeniem kija do bili, jak to sprawdzic?
        predictionBall.GetComponent<Rigidbody>().AddForceAtPosition(cue.transform.forward * cue.force, 
            predictionBall.transform.position, ForceMode.Impulse);
        
        for (int i = 0; i < steps; i++)
        {
            scenePredictionPhysics.Simulate(Time.fixedDeltaTime);

            markerPos[i] = predictionBall.transform.position;
        }
        Destroy(predictionBall);
    }

    private void UpdateBallsPositions()
    {
        for (int i = 0; i < 15; i++)
        {
            ballsPredicted[i].transform.position = ballsPos[i].position;
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
