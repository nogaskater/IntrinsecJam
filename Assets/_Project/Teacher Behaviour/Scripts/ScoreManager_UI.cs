using System;
using UnityEngine;
using TMPro;

public class ScoreManager_UI : MonoBehaviour
{
    [SerializeField] private ScoreManager _scoreManager;

    [SerializeField] private GameObject _live1;
    [SerializeField] private GameObject _live2;
    [SerializeField] private GameObject _live3;

    private void Awake()
    {
        if (_scoreManager == null)
            throw new ArgumentNullException("_scoreManager");

        _scoreManager.OnLiveUpdated += UpdateLives;


        if (_live1 == null)
            throw new ArgumentNullException("_live1");
        if (_live2 == null)
            throw new ArgumentNullException("_live2");
        if (_live3 == null)
            throw new ArgumentNullException("_live3");
    }

    private void Start()
    {
        _live1.SetActive(false);
        _live2.SetActive(false);
        _live3.SetActive(false);
    }


    private void UpdateLives(int lives)
    {
        if(lives == 0)
        {
            _live1.SetActive(false);
            _live2.SetActive(false);
            _live3.SetActive(false);
        }
        else if(lives == 1)
        {
            _live1.SetActive(true);
            _live2.SetActive(false);
            _live3.SetActive(false);
        }
        else if(lives == 2)
        {
            _live1.SetActive(true);
            _live2.SetActive(true);
            _live3.SetActive(false);
        }
        else if(lives == 3)
        {
            _live1.SetActive(true);
            _live2.SetActive(true);
            _live3.SetActive(true);
        }
    }
}
