using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    //BUTTONS
    public List<Button> buttons;
    public List<TMP_Text> backText;

    public void PlayGame()
    {
        //HAY QUE AÑADIR LA ESCENA AL BUILDEAR
        Debug.Log("Empecemos");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
        Debug.Log("Debo cerrarme");
        Application.Quit();
    }

    public void LoadNextScene(string sceneToOpen)
    {
        SceneManager.LoadScene(sceneToOpen);
    }

    //HOVER BUTTON
    public void HoverButton(int numButton)
    {
        LeanTween.scale(buttons[numButton].gameObject, new Vector3(1.2f, 1.2f, 1.2f), 0.2f).setEaseOutBack().setIgnoreTimeScale(true);
    }

    //UNHOVER BUTTON
    public void UnHoverButton(int numButton)
    {
        LeanTween.scale(buttons[numButton].gameObject, new Vector3(1f, 1f, 1f), 0.2f).setIgnoreTimeScale(true);
    }

    //TEXT BACK HOVER
    public void HoverText(int numText)
    {
        LeanTween.scale(backText[numText].gameObject, new Vector3(1.2f, 1.2f, 1.2f), 0.2f).setEaseOutBack().setIgnoreTimeScale(true);
    }

    //TEXT BACK UNHOVER
    public void UnhoverText(int numText)
    {
        LeanTween.scale(backText[numText].gameObject, new Vector3(1f, 1f, 1f), 0.2f).setIgnoreTimeScale(true);
    }
}
