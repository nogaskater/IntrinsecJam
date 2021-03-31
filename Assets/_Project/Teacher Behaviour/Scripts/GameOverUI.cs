using System;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private ScoreManager _scoreManager;

    [SerializeField] private GameObject _table;
    [SerializeField] private GameObject _gameOverCanvas;

    private void Awake()
    {
        if (_scoreManager == null)
            throw new ArgumentNullException("_scoreManager");

        _scoreManager.OnGameOver += GameOver;

        _gameOverCanvas.SetActive(false);
    }

    private void GameOver()
    {
        _table.SetActive(false);

        _gameOverCanvas.SetActive(true);
    }

    public void Button_RestartGame()
    {
        _scoreManager.RestartGame();
    }

}
