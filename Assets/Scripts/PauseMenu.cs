using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update

    //VARIABLEs
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public List<TMP_Text> backText;

    public string MainMenu = "MainMenu";

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        //AudioListener.volume = 1.0f;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        //AudioListener.volume = 0.2f;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        //Debug.Log("Loading Menu...");
        SceneManager.LoadScene(MainMenu);
        //AudioListener.volume = 1.0f;
    }

    public void QuitGame()
    {
        //Debug.Log("QUIT GAME PAUSE MENU");
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
