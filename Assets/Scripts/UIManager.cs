using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public TMP_Text circleCount;
    public TMP_Text ballCount;


    BallHandler ballHandler;
    LevelManager levelManager;

    [Space]
    [Header("GamePlay Ui")]
    public GameObject newLevelUI;
    public TMP_Text levelText;
    public TMP_Text targetText;
    public Image circleImage;

    [Space]
    public GameObject restartUI;
    public Image restatCircleImage;
    public GameObject pauseUI;

    public void newLevelUiFun()
    {
        newLevelUI.SetActive(true);
        levelText.text = "LEVEL " + (LevelManager.currentLevelCount+1).ToString();
        targetText.text = "TARGET " + levelManager.currentLevel.CirclesCount.ToString();
        circleImage.DOFillAmount(1, 1f).From(0f).SetEase(Ease.InOutBack).OnComplete(() =>
        {
            newLevelUI.SetActive(false);
        });
    }
    public void restartUiFun()
    {
        restartUI.SetActive(true);
        circleImage.DOFillAmount(1f, 1f).From(0f).SetEase(Ease.Linear).OnComplete(() =>
        {
            restartUI.SetActive(false);
        });
    }

    private void Awake()
    {
        ballHandler = GameObject.FindObjectOfType<BallHandler>();
        levelManager = GameObject.FindObjectOfType<LevelManager>();
        pauseUI.SetActive(false);
    }
    private void Update()
    {
        circleCount.text = (ballHandler.currentCircleCount + 1).ToString() + "/" + levelManager.currentLevel.CirclesCount.ToString();
        ballCount.text = levelManager.currentLevel.BallsCount[ballHandler.currentCircleCount].ToString();
    }
    public void pause()
    {
        GameObject.FindObjectOfType<soundManager>().fade(0);
        pauseUI.SetActive(true);
    }
    public void resume()
    {
        GameObject.FindObjectOfType<soundManager>().fade(1);
        pauseUI.SetActive(false);
    }
    public void home()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
