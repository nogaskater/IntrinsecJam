using System;
using UnityEngine;

public class StudentScore : MonoBehaviour
{
    [SerializeField] private NPCBallTransitionController _nPCBallTransitionController;

    private void Awake()
    {
        if (_nPCBallTransitionController == null)
            throw new ArgumentNullException("_nPCBallTransitionController");

        _nPCBallTransitionController.OnBallReceived += ModifyScore;
    }

    public int Score = 0;

    public Action<int> OnScoreModified;
    public void ModifyScore(int delta)
    {
        Score += delta;

        OnScoreModified?.Invoke(Score);
    }
}
