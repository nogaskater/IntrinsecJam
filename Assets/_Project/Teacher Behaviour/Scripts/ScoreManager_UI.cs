using System;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ScoreManager_UI : MonoBehaviour
{
    [SerializeField] private ScoreManager _scoreManager;

    public List<GameObject> _lives;

    [SerializeField] private TextMeshProUGUI _scoreText;

    [SerializeField] private TextMeshProUGUI _timeText;

    [SerializeField] private PlayerPopUp _playerPopUpPrefab;
    [SerializeField] private Transform _playerPopUpTransform;

    private void Awake()
    {
        if (_scoreManager == null)
            throw new ArgumentNullException("_scoreManager");

        _scoreManager.OnLiveUpdated += UpdateLives;


        if (_scoreText == null)
            throw new ArgumentNullException("_scoreText");

        _scoreManager.OnScoreUpdated += UpdateScore;


        if (_timeText == null)
            throw new ArgumentNullException("_timeText");

        if (_playerPopUpPrefab == null)
            throw new ArgumentNullException("_playerPopUpPrefab");
        if (_playerPopUpPrefab == null)
            throw new ArgumentNullException("_playerPopUpPrefab");

    }

    private void Start()
    {
        UpdateLives(false);

        UpdateScore(0);
    }

    private void Update()
    {
        _timeText.text = ((int)_scoreManager.CurrentCounter).ToString(); // REPASAR FORMATO
    }


    private void UpdateLives(bool popUp)
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

        if(popUp)
            Instantiate(_playerPopUpPrefab, _playerPopUpTransform.position, Quaternion.identity);
    }

    private void UpdateScore(int score)
    {
        _scoreText.text = "Score: " + score;
    }
}
