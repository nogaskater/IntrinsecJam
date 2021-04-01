using System;
using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private ScoreManager _scoreManager;

    [SerializeField] private GameObject _table;
    [SerializeField] private GameObject _gameOverCanvas;

    [SerializeField] private TextMeshProUGUI _winLoseText;

    private void Awake()
    {
        if (_scoreManager == null)
            throw new ArgumentNullException("_scoreManager");
        if (_table == null)
            throw new ArgumentNullException("_table");
        if (_gameOverCanvas == null)
            throw new ArgumentNullException("_gameOverCanvas");


        if (_winLoseText == null)
            throw new ArgumentNullException("_winLoseText");

        _scoreManager.OnGameOver += GameOver;

        _gameOverCanvas.SetActive(false);
    }

    private void GameOver(bool win)
    {
        if (win)
            _winLoseText.text = "YOU SURVIVED!";
        else
            _winLoseText.text = "GAME OVER...";

        _table.SetActive(false);

        _gameOverCanvas.SetActive(true);
    }

    public void Button_RestartGame()
    {
        _scoreManager.RestartGame();
    }

}
