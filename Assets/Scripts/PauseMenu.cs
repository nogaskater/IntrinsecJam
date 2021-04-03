using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update

    //VARIABLEs
    public GameObject pauseMenuUI;
    public List<TMP_Text> backText;

    public string MainMenu = "MainMenu";

    private bool _paused;



    public void Pause()
    {
        _paused = !_paused;

        pauseMenuUI.SetActive(_paused);

        Time.timeScale = _paused ? 0 : 1;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(MainMenu);
    }

    public void QuitGame()
    {
        Application.Quit();
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
