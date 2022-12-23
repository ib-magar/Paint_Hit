using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[System.Serializable]
public class levelData
{
   public levelData(levelData l)
    {
        CirclesCount = l.CirclesCount;
        BallsCount = new int[CirclesCount];
        for(int i=0;i<CirclesCount;i++)
        {
            BallsCount[i] = l.BallsCount[i];
        }
        colorArray = new Color[CirclesCount];
        for(int i=0;i<CirclesCount;i++)
        {
            colorArray[i] = l.colorArray[i];
        }
        preColoredCount = new int[CirclesCount];
        for(int i=0;i<CirclesCount;i++)
        {
            preColoredCount[i] = l.preColoredCount[i];
        }
        diamondCount = new int[CirclesCount];
        for(int i=0;i<CirclesCount;i++)
        {
            diamondCount[i] = l.diamondCount[i];
        }
    }
    public int CirclesCount;
    public int[] BallsCount;
    public Color[] colorArray;

    public int[] preColoredCount;
    public int[] diamondCount;
};


public class LevelManager : MonoBehaviour
{
    public static int currentLevelCount;
    public levelData[] Levels;
    public levelData currentLevel;

    [Header("scripts")]
    UIManager uiManager;
    private void Awake()
    {
        currentLevel=new levelData(Levels[currentLevelCount]);
        uiManager = GameObject.FindObjectOfType<UIManager>();
    }
    public void restartCurrentLevel()
    {
        StartCoroutine(restartLevel());
    }
    IEnumerator restartLevel()
    {
        yield return new WaitForSeconds(1.5f);
        uiManager.restartUiFun();
        yield return new WaitForSeconds(1f);
            //Deleting the assets
            foreach (Transform t in GameObject.Find("Circles").transform)
            {
                Destroy(t.gameObject);
            }
            foreach (Transform t in GameObject.Find("Balls").transform)
            {
                Destroy(t.gameObject);
            }
        //currentLevel = Levels[currentLevelCount];
        currentLevel = new levelData(Levels[currentLevelCount]);
        GameObject.FindObjectOfType<BallHandler>().Start();
    }
    public void loadNextLevel()
    {
        StartCoroutine(nextLevel());
    }
    
    IEnumerator nextLevel()
    {
        yield return new WaitForSeconds(1.5f);
        if (Levels.Length > currentLevelCount + 1)
        {
            //Deleting the assets
            foreach (Transform t in GameObject.Find("Circles").transform)
            {
                Destroy(t.gameObject);
            }
            foreach (Transform t in GameObject.Find("Balls").transform)
            {
                Destroy(t.gameObject);
            }
            currentLevelCount++;
            //currentLevel = Levels[currentLevelCount];
            currentLevel = new levelData(Levels[currentLevelCount]);
            GameObject.FindObjectOfType<BallHandler>().Start();
        }
        else
        {
            print("Game completed");
            SceneManager.LoadScene("MainMenu");
        }
    }
}
