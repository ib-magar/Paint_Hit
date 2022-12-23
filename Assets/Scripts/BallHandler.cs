using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class BallHandler : MonoBehaviour
{
    public static Color staticBallColor;
    public Transform Circlesparent;
    public Transform BallsParent;
    //Keys
    public KeyCode instantiateKey;

    public GameObject ballPrefab;
    public GameObject ball;
    public Transform LaunchPosition;
    public Color ballColor;
    public float speed;

    [Space]
    [Header("Circle")]
    public float circleDistance;
    public bool canHit=false;


    [Space]
    [Header("Scripts")]
    private LevelManager levelManager;

    [Space]
    [Header("levelData")]
    private int totalCirclesCount;
    public int currentCircleCount;
    private void Awake()
    {
        Circlesparent = GameObject.Find("Circles").GetComponent<Transform>();
        BallsParent = GameObject.Find("Balls").GetComponent<Transform>();
        levelManager = GameObject.FindObjectOfType<LevelManager>();
        ballColor = levelManager.currentLevel.colorArray[0];
        ball.GetComponent<MeshRenderer>().material.color = ballColor;
    }
    public void Start()
    {
        GameObject.FindObjectOfType<UIManager>().newLevelUiFun();
        // staticBallColor = ballColor;
        Invoke("CreateNewCircle", 1.5f);
        AssignLevelData();

        GameObject.FindObjectOfType<soundManager>().fade(1);
    }
    void AssignLevelData()
    {
        totalCirclesCount = levelManager.currentLevel.CirclesCount;
        currentCircleCount = 0;
        ballColor = levelManager.currentLevel.colorArray[0];
        ball.GetComponent<MeshRenderer>().material.color = ballColor;
    }

    public void shoot()
    {
        if (canHit)
        {
            //Creating a new ball instance
            GameObject g = Instantiate(ballPrefab, LaunchPosition.position, Quaternion.identity);
            g.GetComponent<MeshRenderer>().material.color = ballColor;
            g.transform.parent = BallsParent;
            g.GetComponent<Rigidbody>().AddForce(Vector3.forward * speed, ForceMode.Impulse);

            levelManager.currentLevel.BallsCount[currentCircleCount]--;
        }
    }
    public void CreateNewCircle()
    {
        canHit = false;             //can't hit while creating the circle
        
        //ball behaviour
        ball.transform.DOMoveZ(0f, .5f).From(-3f).SetEase(Ease.InOutBounce);
        ball.GetComponent<MeshRenderer>().material.color = levelManager.currentLevel.colorArray[currentCircleCount];

        //Shift the lower circles
        if (Circlesparent.childCount > 0)
        {
            for (int i = Circlesparent.childCount; i>0; i--)
            {
                Circlesparent.GetChild(Circlesparent.childCount-i).transform.DOMoveY(-4 * (i), .1f);
                Circlesparent.GetChild(Circlesparent.childCount-i).GetComponent<circleScript>().stopRotation();      //stopping the rotation of circle
            }
              changeCirlceToGradient();
         }

            //Create new Circle 
            GameObject newCircle = Instantiate(Resources.Load("Round")) as GameObject;
            newCircle.transform.position = LaunchPosition.position + new Vector3(0, LaunchPosition.position.y + 15, circleDistance);
            newCircle.name = "Circle"+currentCircleCount;
            newCircle.transform.parent = Circlesparent;
        
            
    }
    public void changeCirlceToGradient()
    {
        //Color c = Color.Lerp(levelManager.currentLevel.upperColor, levelManager.currentLevel.downColor, (float)((currentCircleCount+1)/ levelManager.currentLevel.CirclesCount));
        Color c = levelManager.currentLevel.colorArray[currentCircleCount];
        Circlesparent.GetChild(Circlesparent.childCount-1).GetComponent<circleScript>().stopRotation();      //stopping the rotation of circle
        //print(levelManager.currentLevel.CirclesCount + " "+ currentCircleCount + "  "+ ((currentCircleCount + 1) / levelManager.currentLevel.CirclesCount));
        foreach (Transform t in Circlesparent.GetChild(Circlesparent.childCount-1))
        {
            t.GetComponent<MeshRenderer>().enabled = true;
            t.GetComponent<MeshRenderer>().material.color = c;
        }
      
    }
}
