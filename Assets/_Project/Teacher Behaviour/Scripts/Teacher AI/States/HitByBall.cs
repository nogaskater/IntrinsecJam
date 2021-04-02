using System;
using UnityEngine;

public class HitByBall : State
{
    [SerializeField] private Patrol _patrolState;

    protected override void Awake()
    {
        base.Awake();

        if (_patrolState == null)
            throw new ArgumentNullException("_patrolState");
    }

    public void Hit(BallController ballController)
    {
        if (_teacherAI.GetState() is HitByBall || _teacherAI.GetState() is FoundBall)
            return;

        _target = ballController.transform;

        ballController.OnEnteredSafeState += RemoveTarget;

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
        if (_target != null)
            _teacherAI.ChangeState(_goToState);
        else
            _teacherAI.ChangeState(_patrolState);
    }



    public override void StartActionAnimation()
    {
        base.StartActionAnimation();

        _teacherAI.Animator.SetTrigger("Hit");

        AudioManager.Instance.PlaySound("Oof");
    }
}
