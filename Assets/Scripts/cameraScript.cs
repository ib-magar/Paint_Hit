using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class cameraScript : MonoBehaviour
{
    public float duration = .1f, strength = .5f, randomness = 90;
    public int vibrato = 10;
    public Transform circlesParent;
    public void shake()
    {
        circlesParent.DOShakePosition(duration , strength, vibrato, randomness );
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            shake();
        }
    }
}
