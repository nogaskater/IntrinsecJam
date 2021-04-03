using System;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ScoreUI : MonoBehaviour
{
    public List<GameObject> _lives;

    [SerializeField] private TextMeshProUGUI _scoreText;

    [SerializeField] private TextMeshProUGUI _timeText;

    [SerializeField] private PlayerPopUp _playerPopUpPrefab;
    [SerializeField] private Transform _playerPopUpTransform;

    private void Awake()
    {
        MatchManager.Instance.OnLiveUpdated += UpdateLives;


        if (_scoreText == null)
            throw new ArgumentNullException("_scoreText");

        MatchManager.Instance.OnScoreUpdated += UpdateScore;


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
        _timeText.text = ((int)MatchManager.Instance.MatchCounter).ToString(); // REPASAR FORMATO
    }


    private void UpdateLives(bool popUp)
    {
        if (_lives.Count < MatchManager.Instance.MaxLives)
            throw new InvalidOperationException("UI Lives is inferior to max lives. More UI elements should be generated.");

        for (int i = 0; i < MatchManager.Instance.MaxLives; i++)
        {
            if (i + 1 <= MatchManager.Instance.CurrentLives)
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
