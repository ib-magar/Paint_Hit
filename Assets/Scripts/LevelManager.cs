using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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

        switch (l.difficultyLevel)
        {
            case 0:
                _timeConstraint = new Vector2(Random.Range(.75f, 1.1f), Random.Range(1.8f, 2.5f));
                _angleConstraint = new Vector2(Random.Range(180f, 210f), Random.Range(350f, 400f));
                break;
            case 1:
                _timeConstraint = new Vector2(Random.Range(.75f, 1.1f), Random.Range(1.8f, 2.5f));
                _angleConstraint = new Vector2(Random.Range(160f, 200f), Random.Range(330f, 370f));
                break;
            case 2:
                _timeConstraint = new Vector2(Random.Range(.75f, 1.1f), Random.Range(1.8f, 2.5f));
                _angleConstraint = new Vector2(Random.Range(140f, 180f), Random.Range(300f, 340f));
                break;
            case 3:
                _timeConstraint = new Vector2(Random.Range(.75f, 1.1f), Random.Range(1.8f, 2.5f));
                _angleConstraint = new Vector2(Random.Range(120f, 150f), Random.Range(270f, 310f));
                break;
                //angle changes
            case 4:
                _timeConstraint = new Vector2(Random.Range(.7f, 1.1f), Random.Range(1.7f, 2.3f));
                _angleConstraint = new Vector2(Random.Range(100f, 150f), Random.Range(250f, 300f));
                break;
            case 5:
                _timeConstraint = new Vector2(Random.Range(.7f, 1.1f), Random.Range(1.5f, 2.1f));
                _angleConstraint = new Vector2(Random.Range(100f, 150f), Random.Range(250f, 300f));
                break;
                //time changes
             case 6:
                _timeConstraint = new Vector2(Random.Range(.7f, 1.1f), Random.Range(1.5f, 2.1f));
                _angleConstraint = new Vector2(Random.Range(100f, 130f), Random.Range(240f, 290f));
                break;
             case 7:
                _timeConstraint = new Vector2(Random.Range(.7f, 1.1f), Random.Range(1.5f, 2.1f));
                _angleConstraint = new Vector2(Random.Range(100f, 120f), Random.Range(220f, 270f));
                break;
             case 8:
                _timeConstraint = new Vector2(Random.Range(.7f, 1.1f), Random.Range(1.5f, 2.1f));
                _angleConstraint = new Vector2(Random.Range(90f, 120f), Random.Range(210f, 250f));
                break;
                //angle changes
            case 9:
                _timeConstraint = new Vector2(Random.Range(.6f, 1f), Random.Range(1.4f, 2.1f));
                _angleConstraint = new Vector2(Random.Range(90f, 120f), Random.Range(210f, 250f));
                break;
            case 10:
                _timeConstraint = new Vector2(Random.Range(.6f, 1f), Random.Range(1.3f, 2f));
                _angleConstraint = new Vector2(Random.Range(90f, 120f), Random.Range(210f, 250f));
                break;
                //time changes
            

            default: Debug.LogError("No difficulty value selected"); break;

        }
        /*_timeConstraint = l._timeConstraint;
        _angleConstraint = l._angleConstraint;*/
    }
    public int CirclesCount;
    public int[] BallsCount;
    public Color[] colorArray;

    public int[] preColoredCount;
    public int[] diamondCount;

    [Range(0,10)] public int difficultyLevel;
    public Vector2 _timeConstraint=new Vector2(.75f,1.5f);
    public Vector2 _angleConstraint=new Vector2(150f,360f);
};


public class LevelManager : MonoBehaviour
{
    public static int currentLevelCount;
    public static bool isContinue=true;
    public levelData[] Levels;
    public levelData currentLevel;

    [SerializeField] Transform _circlesParent;
    [SerializeField] Transform _ballsParent;
    private BallHandler _ballHandler;
    [Header("scripts")]
    UIManager uiManager;
    private void Awake()
    {
        //PlayerPrefs.SetInt("CompletedLevel", 0);
        if(PlayerPrefs.HasKey("CompletedLevel")&&isContinue)
        {
            currentLevelCount = PlayerPrefs.GetInt("CompletedLevel");
        }

        currentLevel=new levelData(Levels[currentLevelCount]);
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
    }
    private void Start()
    {
        _ballHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<BallHandler>();
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
            foreach (Transform t in _circlesParent)
            {
                Destroy(t.gameObject);
            }
            foreach (Transform t in _ballsParent)
            {
                Destroy(t.gameObject);
            }
            currentLevelCount++;
        if(currentLevelCount>PlayerPrefs.GetInt("CompletedLevel"))
        {
            PlayerPrefs.SetInt("CompletedLevel", currentLevelCount);
        }
            //currentLevel = Levels[currentLevelCount];
            currentLevel = new levelData(Levels[currentLevelCount]);
            _ballHandler.Start();
        }
        else
        {
            print("Game completed");
            PlayerPrefs.SetInt("CompletedLevel", 0);
            SceneManager.LoadScene("MainMenu");
        }

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            loadNextLevel();
        }
    }
}
