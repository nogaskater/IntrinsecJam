using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{

    [Header("Settings")]
    [SerializeField] private int _maxLives = 3;
    [SerializeField] private float _maxTime = 60;


    public int MaxLives => _maxLives;

    public int CurrentLives { get; private set; }

    private readonly List<StudentScore> _studentScore = new List<StudentScore>();

    public Action<int> OnScoreUpdated;
    public Action<bool> OnLiveUpdated;
    public Action<bool> OnGameOver;

    public Action OnTimeOut;

    private bool _gameOver = false;

    private float _counter;
    public float CurrentCounter => _counter;

    public int TotalScore
    {
        get
        {
            int sum = 0;
            foreach (var studentScore in _studentScore)
            {
                sum += studentScore.Points;
            }

            return sum;
        }
    }

    private void Awake()
    {
        CurrentLives = _maxLives;

        _counter = _maxTime;
    }

    private void Update()
    {

        if(_counter > 0)
        {
            _counter -= Time.deltaTime;
        }
        else
        {
            if(!_gameOver)
            {
                OnTimeOut?.Invoke();

                GameOver(CurrentLives > 0);
            }

        }
    }

    //private void Update()
    //{
    //    // Debug
    //    if (Input.GetKeyDown(KeyCode.E))
    //        ModifyLives(-1);
    //    else if (Input.GetKeyDown(KeyCode.R))
    //        ModifyLives(1);
    //}

    public void ModifyLives(int delta)
    {
        if (delta > 0 && CurrentLives == _maxLives)
            return;

        if (delta < 0 && CurrentLives == 0)
            return;

        CurrentLives += delta;

        OnLiveUpdated?.Invoke(true);

        if(CurrentLives == 0)
        {
            GameOver(false);
        }
    }

    public void UpdateScore(int grade, int points)
    {
        OnScoreUpdated?.Invoke(TotalScore);
    }
    public void AddNewStudentScore(StudentScore studentScore)
    {
        _studentScore.Add(studentScore);

        studentScore.OnScoreModified += UpdateScore;
    }

    public void GameFinished()
    {
        GameOver(CurrentLives > 0);
    }

    public void GameOver(bool win)
    {
        if (_gameOver)
            return;

        _gameOver = true;

        OnGameOver?.Invoke(win);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
