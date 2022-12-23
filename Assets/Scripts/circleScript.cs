using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class circleScript : MonoBehaviour
{
    public float RotationSpeed;
    public Vector2 speedLimits;
    public GameObject stackEffect;
    public GameObject diamond;

    [Header("scripts")]
    BallHandler ballHandler;
    LevelManager levelManager;
    private Coroutine rotationCoroutine;
    private void Awake()
    {
        ballHandler = GameObject.FindObjectOfType<BallHandler>();
        levelManager = GameObject.FindObjectOfType<LevelManager>();
    }
    private void Start()
    {
        speedLimits = new Vector2(60f, 200f);
        RotationSpeed = Random.Range(speedLimits.x,speedLimits.y);
        preColorCheck();
       // diamondImplemention();
        MoveTo(0);              //moving the circle to the 0 y-axis
        rotationCoroutine = StartCoroutine(rotate());
    }
    public void diamondImplemention()
    {
        for (int i = 0; i < levelManager.currentLevel.diamondCount[ballHandler.currentCircleCount]; i++)
        {
            int randomNo = Random.Range(0, 24);
            Transform t = transform.GetChild(randomNo);
            if (t.name != "colored")
            {
                if (t.name != "diamonded")
                {
                    GameObject g = Instantiate(diamond, t.GetChild(0).position, Quaternion.identity);
                    g.tag = "diamond";
                    g.name = "diamonded";
                    g.transform.parent = t;
                }
                else
               i--;

            }
            else
            i--;
           
        }
    }
    public void preColorCheck()
    {
       
            for(int i=0;i<levelManager.currentLevel.preColoredCount[ballHandler.currentCircleCount];i++)
            {
                int randomNo = Random.Range(0, 24);
                Transform t = transform.GetChild(randomNo);
                t.tag = "color";
                t.name = "colored";
                t.GetComponent<MeshRenderer>().enabled = true;
                t.GetComponent<MeshRenderer>().material.color = levelManager.currentLevel.colorArray[ballHandler.currentCircleCount];
            }
        
    }
    public void setRotationSpeed(float x, float y)
    {
        RotationSpeed = Random.Range(x, y);
    }
    public void setRotationSpeed(float x)
    {
        RotationSpeed = x;
    }

    public void stopRotation()
    {
        if(rotationCoroutine!=null)
        StopCoroutine(rotationCoroutine);
    }
    IEnumerator rotate()
    {                               //Apply the complexity
        while(true)
        {
            float time = Random.Range(.75f, 1.5f);
            transform.DORotate(new Vector3(transform.eulerAngles.x,Random.Range(150f,360f),transform.eulerAngles.z), time);
            yield return new WaitForSeconds(time);
        }
    }
    public void MoveTo(Vector3 position)
    {
        transform.DOMove(position, .5f).SetEase(Ease.InOutBack);
    }           //Moving the circle to the target position
    public void MoveTo(float y)
    {
        
            transform.DOMoveY(y, .5f).SetEase(Ease.InOutBounce).OnComplete(() =>
            {
                Camera.main.GetComponent<cameraScript>().shake();       //shake camera
                       
                ballHandler.canHit = true;
                if (ballHandler.currentCircleCount > 0)
                {
                    GameObject g = Instantiate(stackEffect, transform.position + new Vector3(0,-2f,0), stackEffect.transform.rotation);
                    g.GetComponent<SpriteRenderer>().color = ballHandler.ballColor;
                    g.transform.DOScale(Vector3.one * 18f, .5f).From(Vector3.zero);
                    g.GetComponent<SpriteRenderer>().DOFade(0f, .5f).From(1f).OnComplete(() =>
                    {
                        Destroy(g);
                    });

                    /*g.GetComponent<ParticleSystem>().startColor = ballHandler.ballColor;
                    Destroy(g, 1f);*/
                }
            });
        
    }
}
