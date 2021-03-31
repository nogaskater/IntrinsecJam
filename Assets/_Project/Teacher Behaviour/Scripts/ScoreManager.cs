using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{

    [Header("Settings")]
    [SerializeField] private int _maxLives = 3;


    public int MaxLives => _maxLives;

    public int CurrentLives { get; private set; }

    private readonly List<StudentScore> _studentScore = new List<StudentScore>();

    public Action<int> OnScoreUpdated;
    public Action<int> OnLiveUpdated;
    public Action OnGameOver;

    private bool _gameOver = false;

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
    }

    private void Update()
    {
        // Debug
        if (Input.GetKeyDown(KeyCode.E))
            ModifyLives(-1);
        else if (Input.GetKeyDown(KeyCode.R))
            ModifyLives(1);
    }

    public void ModifyLives(int delta)
    {
        if (delta > 0 && CurrentLives == _maxLives)
            return;

        if (delta < 0 && CurrentLives == 0)
            return;

        CurrentLives += delta;

        OnLiveUpdated?.Invoke(CurrentLives);

        if(CurrentLives == 0)
        {
            GameOver();
        }
    }

    public void UpdateScore(int grage, int points)
    {
        OnScoreUpdated?.Invoke(TotalScore);
    }

    public void AddNewStudentScore(StudentScore studentScore)
    {
        _studentScore.Add(studentScore);

        studentScore.OnScoreModified += UpdateScore;
    }

    public void GameOver()
    {
        OnGameOver?.Invoke();

        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
