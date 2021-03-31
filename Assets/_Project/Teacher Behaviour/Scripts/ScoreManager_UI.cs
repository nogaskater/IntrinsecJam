using System;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ScoreManager_UI : MonoBehaviour
{
    [SerializeField] private ScoreManager _scoreManager;

    public List<GameObject> _lives;

    [SerializeField] private TextMeshProUGUI _scoreText;

    private void Awake()
    {
        if (_scoreManager == null)
            throw new ArgumentNullException("_scoreManager");

        _scoreManager.OnLiveUpdated += UpdateLives;


        if (_scoreText == null)
            throw new ArgumentNullException("_scoreText");

        _scoreManager.OnScoreUpdated += UpdateScore;
    }

    private void Start()
    {
        UpdateLives(_scoreManager.CurrentLives);

        UpdateScore(0);
    }


    private void UpdateLives(int lives)
    {
        if (_lives.Count < _scoreManager.MaxLives)
            throw new InvalidOperationException("UI Lives is inferior to max lives. More UI elements should be generated.");

        for (int i = 0; i < _scoreManager.MaxLives; i++)
        {
            if (i + 1 <= _scoreManager.CurrentLives)
                _lives[i].SetActive(true);
            else
                _lives[i].SetActive(false);
        }

    }

    private void UpdateScore(int score)
    {
        _scoreText.text = "Score: " + score;
    }
}
