using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BallScript : MonoBehaviour
{
    LevelManager levelManager;
    BallHandler ballHandler;
    UIManager _uiManager;

    
    private void Awake()
    {
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        ballHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<BallHandler>();
        _uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
    }
    private void Start()
    {
        Destroy(gameObject, 2f);
    }
    private void OnTriggerEnter(Collider other)
    {
       if (other.tag.Equals("diamond"))
        {
            //get the diamond
            Instantiate(Resources.Load("DiamondSplit"), other.transform.position, Quaternion.identity);
            soundManager.instance.DiamondCollectedSound();
            DataManager.instance.IncrementDiamond();
            Destroy(other.gameObject);

            _uiManager.UpdateDiamonds();
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        Camera.main.GetComponent<cameraScript>().shake();
        GetComponent<SphereCollider>().enabled = false;
        if(other.collider.tag.Equals("color"))
        {
            
            soundManager.instance.WrongHit();
            //game over  or 1 life lost
            GetComponent<MeshRenderer>().material.color = new Color(1, 0, 0, 1);
            GetComponent<TrailRenderer>().startColor = new Color(1, 0, 0, 1);
            splash(other,Color.red);
            other.collider.GetComponent<MeshRenderer>().material.color = Color.red;
            //GetComponent<Rigidbody>().AddForce(Vector3.down * 150f);
            Destroy(gameObject,.5f);

                //Restart
            ballHandler.canHit = false;
            levelManager.restartCurrentLevel();

            DataManager.instance.IncrementFails();
        }
        else
        {
            soundManager.instance.Splash();
            Color c = levelManager.currentLevel.colorArray[ballHandler.currentCircleCount];
            
            splash(other, c);
            StartCoroutine(changeColor(other.gameObject));

            DataManager.instance.IncrementSuccesses();
        }
        
    }
    public void splash(Collision other,Color c)
    {
        //Generate the splash and then destroy ball
        GameObject splash = Instantiate(Resources.Load("splash1")) as GameObject;
        c.a = 1f;
        splash.transform.parent = other.transform;
        splash.GetComponent<SpriteRenderer>().color = c;
        splash.transform.GetChild(0).GetComponent<ParticleSystem>().startColor = c;
        splash.transform.DOScale(Vector3.one * 1f, .18f).From(Vector3.one * .2f).OnComplete(() => 
        {
            splash.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(splash,.175f);
        
        });
        splash.transform.position = other.transform.position + new Vector3(0, 0, -9.8f);
    }

    public  IEnumerator changeColor(GameObject g)
    {
        yield return new WaitForSeconds(.02f);
        // Change the color of the object 
        //change the tag
        g.name = "colored";
        g.tag = "color";
        GetComponent<MeshRenderer>().enabled = false;
            yield return new WaitForSeconds(.1f);
            g.GetComponent<MeshRenderer>().enabled = true;
        g.GetComponent<MeshRenderer>().material.color = levelManager.currentLevel.colorArray[ballHandler.currentCircleCount];

            //After changing the color check the balls counts
          if (levelManager.currentLevel.BallsCount[ballHandler.currentCircleCount] <= 0)
          {
            if (ballHandler.currentCircleCount + 1 < levelManager.currentLevel.CirclesCount)
            {
                ballHandler.currentCircleCount++;
                ballHandler.CreateNewCircle();
            }
            else
            {
                ballHandler.canHit = false;
                ballHandler.changeCirlceToGradient();
                print("level completed "+LevelManager.currentLevelCount);

                yield return new WaitForSeconds(1f);
                levelManager.loadNextLevel();
                // Assign new level Data 
            }
          }
        Destroy(gameObject);
    }
}
