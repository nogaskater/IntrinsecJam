using System;
using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject _table;
    [SerializeField] private GameObject _gameOverCanvas;

    [SerializeField] private TextMeshProUGUI _winLoseText;

    private void Awake()
    {
        if (_table == null)
            throw new ArgumentNullException("_table");
        if (_gameOverCanvas == null)
            throw new ArgumentNullException("_gameOverCanvas");


        if (_winLoseText == null)
            throw new ArgumentNullException("_winLoseText");

        MatchManager.Instance.OnGameOver += GameOver;

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
        MatchManager.Instance.RestartGame();
    }

}
