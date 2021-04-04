using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlay : MonoBehaviour
{

    [SerializeField] GameObject howToPlay;
    [SerializeField] GameObject mainMenu;

    [Header("TUTORIALS")]
    [SerializeField] GameObject[] Tutorials;

    int currentTutorial = 0;

    [Header("Arrows")]
    [SerializeField] private GameObject _leftArrow;
    [SerializeField] private GameObject _rightArrow;

    private void Start()
    {
        CloseHowToPlay();
    }

    public void OpenHowToPlay()
    {
        howToPlay.SetActive(true);
        mainMenu.SetActive(false);

        currentTutorial = 0;

        ShowTutorial(currentTutorial);
    }

    public void CloseHowToPlay()
    {
        howToPlay.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void MoveTutorialRight()
    {
        currentTutorial++;
        ShowTutorial(currentTutorial);
    }
    public void MoveTutorialLeft()
    {
        currentTutorial--;
        ShowTutorial(currentTutorial);
    }

    private void ShowTutorial(int id)
    {
        _leftArrow.SetActive(true);
        _rightArrow.SetActive(true);

        if (currentTutorial == 0)
        {
            _leftArrow.SetActive(false);
        }
        else if (currentTutorial == Tutorials.Length - 1)
        {
            _rightArrow.SetActive(false);
        }

        for (int i = 0; i < Tutorials.Length; i++)
        {
            if (i == id)
            {
                Tutorials[i].SetActive(true);
            }
            else
            {
                Tutorials[i].SetActive(false);
            }
        }
    }
}
