using System;
using UnityEngine;
using TMPro;

public class ScorePopUp : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    [SerializeField] private float _duration;
    [SerializeField] private float _speed;

    private float _counter = 0;

    private void Awake()
    {
        if (_scoreText == null)
            throw new ArgumentNullException("_scoreText");

    }
    public void Initialize(string score)
    {
        _scoreText.text = score;
    }

    private void Update()
    {
        _counter += Time.deltaTime;

        if (_counter > _duration)
            Destroy(gameObject);

        transform.Translate(Vector3.up * _speed * Time.deltaTime);

    }

}
