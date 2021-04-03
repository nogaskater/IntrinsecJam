using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MatchManager : MonoBehaviour
{
    private const int MAX_LIVES = 3;

    public static MatchManager Instance;

    [Header("Settings")]
    [SerializeField] private float _matchTime = 60;

    public int MaxLives => MAX_LIVES;
    public int CurrentLives { get; private set; }
    public float MatchCounter { get; private set; }

    private bool _gameOver = false;

    private int _score = 0;


    // EVENTS
    public Action<bool> OnLiveUpdated; // popupFeedback
    public Action<int> OnScoreUpdated; // score
    public Action<bool> OnGameOver; // win

    public Action OnTimeOut;
    


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
    }

    private void Update()
    {
        if(MatchCounter > 0)
        {
            MatchCounter -= Time.deltaTime;
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

    public void StartMatch()
    {
        CurrentLives = MAX_LIVES;

        MatchCounter = _matchTime;

        _score = 0;
    }

    public void ModifyLives(int delta)
    {
        if (delta > 0 && CurrentLives == MAX_LIVES)
            return;

        if (delta < 0 && CurrentLives == 0)
            return;

        CurrentLives += delta;

        if (CurrentLives <= 0)
            CurrentLives = 0;
        else if (CurrentLives >= MAX_LIVES)
            CurrentLives = MAX_LIVES;

        OnLiveUpdated?.Invoke(true);

        AudioManager.Instance.PlaySound("LoseLife");

        if(CurrentLives == 0)
        {
            GameOver(false);
        }

    }

    public void AddScore(int delta)
    {
        _score += delta;

        OnScoreUpdated?.Invoke(_score);
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

        StartMatch();
    }
}
