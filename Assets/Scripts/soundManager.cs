using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Security.Cryptography;
using System.Runtime.CompilerServices;

public class soundManager : MonoBehaviour
{
    public static soundManager instance;
    private void Awake()
    {
        //GameObject s = GameObject.FindGameObjectWithTag("SoundManager");
        if(instance!=null)
        {
            Destroy(this);
        }  
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this);
    }


    public AudioSource musicSource;
    public AudioSource effectSource;

    public AudioClip splash;
    public AudioClip stack;
    [SerializeField] AudioClip _buttonPressedSound;
    [SerializeField] AudioClip _stackedSound;
    [SerializeField] AudioClip _wrongHitSound;
    [SerializeField] AudioClip _diamondCollectedSound;

    public float fadeTime;

    public Vector2 soundLimit;
    private void Start()
    {
        musicSource.DOFade(PlayerPrefs.GetFloat("MusicVolume"), fadeTime).From(0f);
        effectSource.volume = PlayerPrefs.GetFloat("FxVolume");
    }
    public void fadeIn()
    {
        musicSource.DOFade(PlayerPrefs.GetFloat("MusicVolume"), fadeTime).From(0f);
    }
    public void fadeOut()
    {
        musicSource.DOFade(soundLimit.x, .1f);
    }
    public void SetVolumes()
    {
        musicSource.volume = PlayerPrefs.GetFloat("MusicVolume");
        effectSource.volume = PlayerPrefs.GetFloat("FxVolume");
    }
    public void SetMusicVolume(float value)
    {
        PlayerPrefs.SetFloat("MusicVolume", value);
        SetVolumes();
    }
    public void SetFxVolume(float value)
    {
        PlayerPrefs.SetFloat("FxVolume", value);
        SetVolumes();
    }
    public void Splash()
    {
        effectSource.PlayOneShot(splash);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            Splash();
    }
    public void ButtonPressed()
    {
        effectSource.PlayOneShot(_buttonPressedSound);
    }
    public void Stacked()
    {
        effectSource.PlayOneShot(_stackedSound);
    }
    public void WrongHit()
    {
        effectSource.PlayOneShot(_wrongHitSound);
    }
    public void DiamondCollectedSound()
    {
        effectSource.PlayOneShot(_diamondCollectedSound);
    }
}
