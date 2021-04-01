﻿using System;
using UnityEngine;

public class HitByBall : State
{
    private Transform _ballTarget;


    public void Hit(Transform ballTarget)
    {
        if (_teacherAI.GetState() is HitByBall)
            return;

        if (_teacherAI.GetState() is FoundBall)
            return;

        _ballTarget = ballTarget;

        _target = transform;

        _teacherAI.ChangeState(this);
    }

    public override void EnterState()
    {
        base.EnterState();

        _teacherAI.SetActiveExclamation(true);
    }

    public override void ExitState()
    {
        base.ExitState();

        _teacherAI.SetActiveExclamation(false);
    }

    public override void FinishAction()
    {
        FoundBall foundBall = _goToState as FoundBall;

        foundBall.BallDetected(_ballTarget);
    }

    public override void StartActionAnimation()
    {
        base.StartActionAnimation();

        _teacherAI.Animator.SetTrigger("Hit");
    }
}
