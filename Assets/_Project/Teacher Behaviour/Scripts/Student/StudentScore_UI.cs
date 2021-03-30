using System;
using UnityEngine;
using TMPro;
public class StudentScore_UI : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private StudentScore _studentScore;

    [SerializeField] private TextMeshProUGUI _studentScoreText;

    private void Awake()
    {
        if (_studentScore == null)
            throw new ArgumentNullException("_studentScore");

        if (_studentScoreText == null)
            throw new ArgumentNullException("_studentScoreText");
    }

    private void OnEnable()
    {
        _studentScore.OnScoreModified += ModifyScoreText;
    }
    private void OnDisable()
    {
        _studentScore.OnScoreModified -= ModifyScoreText;

    }

    private void ModifyScoreText(int score)
    {
        _studentScoreText.text = score + "/10";
    }
}