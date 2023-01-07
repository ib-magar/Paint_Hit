using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    private void Awake()
    {
        if(instance!=null)
        {
            Destroy(instance);
        }
        else
        instance = this;

        CheckData();
    }
    void CheckData()
    {
        if(!PlayerPrefs.HasKey("Diamonds"))
        {
            PlayerPrefs.SetInt("Diamonds", 0);
        }
        if(!PlayerPrefs.HasKey("CompletedLevel"))
        {
            PlayerPrefs.SetInt("CompletedLevel",0);
        }
        if(!PlayerPrefs.HasKey("MusicVolume"))
        {
        PlayerPrefs.SetFloat("MusicVolume", .3f);
        }
        if(!PlayerPrefs.HasKey("FxVolume"))
        {
        PlayerPrefs.SetFloat("FxVolume", 1f);

        }
        if (!PlayerPrefs.HasKey("Attempts"))
        {
            PlayerPrefs.SetInt("Attempts", 0);
        }
        if (!PlayerPrefs.HasKey("Success"))
        {
            PlayerPrefs.SetInt("Success", 0);
        }
        if (!PlayerPrefs.HasKey("Fails"))
        {
            PlayerPrefs.SetInt("Fails", 0);
        }
        if (!PlayerPrefs.HasKey("PaintBalls"))
        {
            PlayerPrefs.SetInt("PaintBalls", 0);
        }
        if (!PlayerPrefs.HasKey("CircleFilled"))
        {
            PlayerPrefs.SetInt("CircleFilled", 0);
        }
        if(PlayerPrefs.HasKey("TimeSpent"))
        {
            PlayerPrefs.SetFloat("TimeSpent", 0f);
        }


    }



    public  void IncrementDiamond()=>  PlayerPrefs.SetInt("Diamonds", PlayerPrefs.GetInt("Diamonds") + 1);

    public void IncrementAttempts() =>  PlayerPrefs.SetInt("Attempts", PlayerPrefs.GetInt("Attempts") + 1);
    public void IncrementSuccesses() => PlayerPrefs.SetInt("Success", PlayerPrefs.GetInt("Success") + 1);
    public void IncrementFails() => PlayerPrefs.SetInt("Fails", PlayerPrefs.GetInt("Fails") + 1);
    public void IncrementPaintBalls() => PlayerPrefs.SetInt("PaintBalls", PlayerPrefs.GetInt("PaintBalls") + 1);
    public void IncrementCircleFills() => PlayerPrefs.SetInt("CircleFilled", PlayerPrefs.GetInt("CircleFilled") + 1);
    
}
