using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BallScript : MonoBehaviour
{
    LevelManager levelManager;
    BallHandler ballHandler;

    private void Awake()
    {
        levelManager = GameObject.FindObjectOfType<LevelManager>();
        ballHandler = GameObject.FindObjectOfType<BallHandler>();
    }
    private void OnTriggerEnter(Collider other)
    {
       if (other.tag.Equals("diamond"))
        {
            //get the diamond
            print("diamond got!");
            Destroy(other.gameObject);
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        GameObject.FindObjectOfType<soundManager>().Splash();

        Camera.main.GetComponent<cameraScript>().shake();
        GetComponent<SphereCollider>().enabled = false;
        if(other.collider.tag.Equals("color"))
        {
            //game over  or 1 life lost
            GetComponent<MeshRenderer>().enabled = false;
            splash(other,Color.red);
            other.collider.GetComponent<MeshRenderer>().material.color = Color.red;
            //GetComponent<Rigidbody>().AddForce(Vector3.down * 150f);
            Destroy(gameObject,.3f);

                //Restart
            ballHandler.canHit = false;
            levelManager.restartCurrentLevel();

        }
        else
        {
            Color c = levelManager.currentLevel.colorArray[ballHandler.currentCircleCount];

            splash(other, c);
            StartCoroutine(changeColor(other.gameObject));
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
        splash.transform.DOScale(Vector3.one * 1.1f, .15f).From(Vector3.one * .2f).OnComplete(() => { Destroy(splash); });
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
    void checkTheballs()
    {

    }
}
