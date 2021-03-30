using System;
using UnityEngine;

public class StudentScore : MonoBehaviour
{
    public int Score = 0;

    public Action<int> OnScoreModified;
    public void ModifyScore(int delta)
    {
        Score += delta;

        OnScoreModified?.Invoke(Score);
    }
}
