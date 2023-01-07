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
    public TMP_Text _diamondsText;

    [SerializeField] Image _upFade;
    [SerializeField] Image _downFade;

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

        _downFade.color = levelManager.currentLevel.colorArray[0];
        _upFade.color = levelManager.currentLevel.colorArray[levelManager.currentLevel.colorArray.Length - 1];
       
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
    private void Start()
    {
        UpdateDiamonds();
    }
    private void Update()
    {
        circleCount.text = (ballHandler.currentCircleCount + 1).ToString() + "/" + levelManager.currentLevel.CirclesCount.ToString();
        ballCount.text = levelManager.currentLevel.BallsCount[ballHandler.currentCircleCount].ToString();

    }
    public void pause()
    {
        soundManager.instance.musicSource.volume = .05f;
        pauseUI.SetActive(true);
    }
    public void resume()
    {
        soundManager.instance.fadeIn();
        pauseUI.SetActive(false);
    }
    public void home()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void UpdateDiamonds()
    {
        _diamondsText.text = PlayerPrefs.GetInt("Diamonds").ToString();
    }
}
