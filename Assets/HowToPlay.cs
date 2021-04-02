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



    public void OpenHowToPlay()
    {
        howToPlay.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void CloseHowToPlay()
    {
        howToPlay.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void MoveTutorialRight()
    {

        currentTutorial++;
        if (currentTutorial >= 4)
        {
            currentTutorial = 0;
        }

        for (int i = 0; i < 4; i++)
        {
            if (i == currentTutorial)
            {
                Tutorials[i].SetActive(true);
            }
            else
            {
                Tutorials[i].SetActive(false);
            }
        }
    }

    public void MoveTutorialLeft()
    {
        currentTutorial--;
        if (currentTutorial < 0 )
        {
            currentTutorial = 3;
        }

        for (int i = 0; i < 4; i++)
        {
            if (i == currentTutorial)
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
