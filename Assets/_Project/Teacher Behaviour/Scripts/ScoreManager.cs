using System;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int Lives = 3;

    private readonly List<StudentScore> _studentScore = new List<StudentScore>();

    public Action<int> OnScoreUpdated;

    public Action<int> OnLiveUpdated;

    public int TotalScore
    {
        get
        {
            int sum = 0;
            foreach (var studentScore in _studentScore)
            {
                sum += studentScore.Score;
            }

            return sum;
        }
    }

    public void ModifyLives()
    {
        Lives--;

        OnLiveUpdated?.Invoke(Lives);
    }

    public void UpdateScore(int score)
    {
        OnScoreUpdated?.Invoke(TotalScore);
    }

    public void AddNewStudentScore(StudentScore studentScore)
    {
        _studentScore.Add(studentScore);

        studentScore.OnScoreModified += UpdateScore;
    }
}
