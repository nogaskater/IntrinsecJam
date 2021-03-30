using System;
using UnityEngine;

public class HittedByBall : State
{
    [SerializeField] private FoundBall _foundBallState;

    private Transform _ballTarget;

    private void Awake()
    {
        if (_foundBallState == null)
            throw new ArgumentNullException("_foundBallState");
    }

    public void Hit(Transform ballTarget)
    {
        _ballTarget = ballTarget;

        _target = transform;

        _teacherAI.ChangeState(this);
    }

    public override void FinishAction()
    {
        _foundBallState.BallDetected(_ballTarget);
    }
}