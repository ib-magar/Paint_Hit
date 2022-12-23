using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{

    public GameObject[] pages;
    public Image[] buttons;
    public RectTransform circle;


   
    private void Start()
    {
        currentOption(2);
    }
    public void currentOption(int n)
    {

        Color mainColor = buttons[n].color;
        mainColor.a = 1;
        buttons[n].color = mainColor;

        circle.position = buttons[n].GetComponent<RectTransform>().position;
        pages[n].SetActive(true);
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
        GameObject.FindObjectOfType<soundManager>().fade(0);
        StartCoroutine(loadGameCoroutine());
    }
    IEnumerator loadGameCoroutine()
    {
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene("Gameplay");

    }



}
