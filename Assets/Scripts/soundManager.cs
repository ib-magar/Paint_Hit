using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class soundManager : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource effectSource;

    public AudioClip splash;
    public AudioClip stack;

    public float fadeTime;

    public Vector2 soundLimit;

    private void Start()
    {
        DontDestroyOnLoad(this);
        fade(1);
    }
    public void fade(int n)
    {
        if (n == 1)
            musicSource.DOFade(soundLimit.y, fadeTime);
        else
            musicSource.DOFade(soundLimit.x, fadeTime);
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
}
