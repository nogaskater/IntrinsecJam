using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Settings")]
    [SerializeField] private int _maxLives = 3;
    [SerializeField] private float _matchTime = 60;

    public int MaxLives => _maxLives;
    public int CurrentLives { get; private set; }
    public float MatchCounter { get; private set; }

    private readonly List<StudentScore> _studentScores = new List<StudentScore>();

    private bool _gameOver = false;


    // EVENTS
    public Action<bool> OnLiveUpdated; // popupFeedback
    public Action<int> OnScoreUpdated; // score
    public Action<bool> OnGameOver; // win

    public Action OnTimeOut;
    
    public int TotalScore
    {
        get
        {
            int sum = 0;
            foreach (var studentScore in _studentScores)
            {
                sum += studentScore.Points;
            }

            return sum;
        }
    }


    private void Awake()
    {
        #region Singletone
        if (Instance != null)
        {
            Destroy(gameObject);

            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);

        #endregion

        StartMatch();
    }

    public void StartMatch()
    {
        CurrentLives = _maxLives;

        MatchCounter = _matchTime;
    }

    public void ModifyLives(int delta)
    {
        if (delta > 0 && CurrentLives == _maxLives)
            return;

        if (delta < 0 && CurrentLives == 0)
            return;

        CurrentLives += delta;

        if (CurrentLives <= 0)
            CurrentLives = 0;
        else if (CurrentLives >= _maxLives)
            CurrentLives = _maxLives;

        OnLiveUpdated?.Invoke(true);

        AudioManager.Instance.PlaySound("LoseLife");

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
        _studentScores.Add(studentScore);

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

        if (win)
        {
            AudioManager.Instance.PlaySound("Win");
        }
        else
            AudioManager.Instance.PlaySound("Lose");


        _gameOver = true;

        OnGameOver?.Invoke(win);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
