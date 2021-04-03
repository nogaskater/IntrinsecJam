using System;
using UnityEngine;

public class HitByBall : State
{
    public void Hit(BallController ballController)
    {
        if (_teacherAI.GetState() is HitByBall || _teacherAI.GetState() is FoundBall)
            return;

        _target = ballController.transform;

        _teacherAI.ChangeState(this);
    }

    public override void EnterState()
    {
        base.EnterState();

        _teacherAI.SetActiveExclamation(true);
    }

    public override void UpdateState()
    {
        if (!_isNearTarget)
        {
            _isNearTarget = true;

            StartActionAnimation();
        }
        else
        {
            _actionCounter += Time.deltaTime;

            if (_actionCounter > _actionTime)
            {
                FinishAction();
            }
        }
    }

    public override void ExitState()
    {
        base.ExitState();

        _teacherAI.SetActiveExclamation(false);
    }

    public override void FinishAction()
    {
        FoundBall foundBallState = _goToState as FoundBall;

        if (_target != null)
            foundBallState.BallDetected(_target.GetComponent<BallController>());
        else
            _teacherAI.ChangeState(_goToState);
    }

    public override void StartActionAnimation()
    {
        base.StartActionAnimation();

        _teacherAI.Animator.SetTrigger("Hit");

        AudioManager.Instance.PlaySound("Oof");
    }
}
