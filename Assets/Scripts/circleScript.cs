using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class circleScript : MonoBehaviour
{
    public Transform point;

    [Header("rotation values")]
    public Vector2 _timeConstraint=new Vector2(.75f,1.5f);
    public Vector2 _angleConstraint=new Vector2(150f,360f);
    
    public GameObject stackEffect;
    public GameObject diamond;

    [Header("scripts")]
    BallHandler ballHandler;
    LevelManager levelManager;
    private Coroutine rotationCoroutine;

    private List<Transform> diamondsList= new List<Transform>();
    private void Start()
    {
        ballHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<BallHandler>();
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        preColorCheck();
        diamondImplemention();
        MoveTo(0);              //moving the circle to the 0 y-axis
        rotationCoroutine = StartCoroutine(rotate());
    }
    public void diamondImplemention()
    {
        for (int i = 0; i < levelManager.currentLevel.diamondCount[ballHandler.currentCircleCount]; i++)
        {
            int randomNo = Random.Range(0, transform.childCount-3);
            Transform t = transform.GetChild(randomNo);
            if (t.name != "colored")
            {
                if (t.name != "diamonded")
                {
                    //Vector3 direction = (transform.GetChild(transform.childCount - 1).position - t.position).normalized;
                    GameObject g = Instantiate(diamond, t.GetChild(0).position, diamond.transform.rotation);
                    //GameObject g = Instantiate(diamond, t.transform.localPosition+ direction*5f, diamond.transform.rotation);
                    g.tag = "diamond";
                    t.name = "diamonded";
                    g.transform.parent = t;
                    diamondsList.Add(g.transform);
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
    public void removeDiamonds()
    {
        foreach(Transform t in diamondsList)
        {
            if (t != null) Destroy(t.gameObject);
        }
    }
    public void stopRotation()
    {
        if(rotationCoroutine!=null)
        StopCoroutine(rotationCoroutine);
    }
    public void SetConstraints(Vector2 TimeConstraint ,Vector2 AngleConstraint)
    {
        _timeConstraint = TimeConstraint;           //.75  to 1.5
        _angleConstraint = AngleConstraint;         // 150 to 360
    }
    IEnumerator rotate()
    {                               //Apply the complexity
        while(true)
        {
            float time = Random.Range(_timeConstraint.x, _timeConstraint.y);
            if(Random.value<.5f)
            transform.DORotate(new Vector3(transform.eulerAngles.x,transform.eulerAngles.y+Random.Range(_angleConstraint.x,_angleConstraint.y),transform.eulerAngles.z), time);
            else
            transform.DORotate(new Vector3(transform.eulerAngles.x,transform.eulerAngles.y-Random.Range(_angleConstraint.x,_angleConstraint.y),transform.eulerAngles.z), time);

            yield return new WaitForSeconds(time);
        }
    }
               //Moving the circle to the target position
    public void MoveTo(Vector3 position) => transform.DOMove(position, .5f).SetEase(Ease.InOutBack);
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
