using System.Collections;
using System.Collections.Generic;
//using UnityEditor.U2D.Sprites;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
//using UnityEditor.UI;

public class MainMenu : MonoBehaviour
{

    public GameObject[] pages;
    public Image[] buttons;
    public RectTransform circle;

    [SerializeField] TMP_Text _currentLevelText;
    [SerializeField] TMP_Text _diamondsText;


    [SerializeField] Slider _musicSlider;
    [SerializeField] Slider _fxSlider;

    [Header("statistics")]
    [SerializeField] TMP_Text _attempts;
    [SerializeField] TMP_Text _successes;
    [SerializeField] TMP_Text _fails;
    [SerializeField] TMP_Text _ballsFired;
    [SerializeField] TMP_Text _circlesFilled;
    [SerializeField] TMP_Text _timeSpent;
    //hello

    [SerializeField] Animator _playButtonAnimator;

    private void Awake()
    {
        
    }
    private void Start()
    {
        _musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        _fxSlider.value = PlayerPrefs.GetFloat("FxVolume");
        //_playButtonAnimator.SetTrigger("pressed");

        UpdateUIValues();
    }
    public void UpdateUIValues()
    {
        currentOption(2);           //home icon selected
        _currentLevelText.text = "Level " + (PlayerPrefs.GetInt("CompletedLevel")+1).ToString();
        _diamondsText.text = PlayerPrefs.GetInt("Diamonds").ToString();

        _attempts.text = PlayerPrefs.GetInt("Attempts").ToString();
        _successes.text = PlayerPrefs.GetInt("Success").ToString();
        _fails.text = PlayerPrefs.GetInt("Fails").ToString();
        _ballsFired.text = PlayerPrefs.GetInt("PaintBalls").ToString();
        _circlesFilled.text = PlayerPrefs.GetInt("CircleFilled").ToString();
        _timeSpent.text = PlayerPrefs.GetFloat("TimeSpent").ToString("F0");   
    }
    public void SetMusic()
    {
        soundManager.instance.SetMusicVolume(_musicSlider.value);
    }
    public void SetFx()
    {
        soundManager.instance.SetFxVolume(_fxSlider.value);
    }
    public void currentOption(int n)
    {

        Color mainColor = buttons[n].color;
        mainColor.a = 1;
        buttons[n].color = mainColor;

        circle.position = buttons[n].GetComponent<RectTransform>().position;
        pages[n].SetActive(true);
        soundManager.instance.ButtonPressed();
        for(int i=0;i<5;i++)
        {
            if (i == n)
                continue;
            Color c = buttons[i].color;
            c.a = .5f;
            buttons[i].color = c;
        }
        for(int i=0;i<5;i++)
        {
            if (i == n)
                continue;
            pages[i].SetActive(false);
        }

    }
    public void loadGame()
    {
        StartCoroutine(loadGameCoroutine(true));
    }
    public void LoadLevel(int _level)
    {
        soundManager.instance.fadeIn();
        StartCoroutine(loadGameCoroutine(false, _level));
    }
    IEnumerator loadGameCoroutine(bool _isContinue,int n=0)
    {
        yield return new WaitForSeconds(.5f);
        if(!_isContinue)
        {
        LevelManager.isContinue = false;
        LevelManager.currentLevelCount = 2;
        }
        else
        {
            LevelManager.isContinue = true;
        }

        SceneManager.LoadScene("Gameplay");

    }
    public void PlayButtonAnimation()
    {
        _playButtonAnimator.SetTrigger("pressed");
    }
    public void ButtonPressed() => soundManager.instance.ButtonPressed();

    public void QuitGame()
    {
        Application.Quit();
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("TimeSpent", PlayerPrefs.GetFloat("TimeSpent") + Time.time);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            PlayButtonAnimation();
        }
    }
}
