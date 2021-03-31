using System;
using UnityEngine;

public class FoundBall : State
{
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private GeneralBallManager _generalBallManager;

    protected override void Awake()
    {
        base.Awake();

        if (_scoreManager == null)
            throw new ArgumentNullException("_scoreManager");
        if (_generalBallManager == null)
            throw new ArgumentNullException("_generalBallManager");
    }

    public void BallDetected(Transform ballTarget)
    {
        if (_teacherAI.GetState() is FoundBall)
            return;

        _target = ballTarget;

        _teacherAI.ChangeState(this);
    }

    public override void FinishAction()
    {
        if (_target.gameObject.layer == LayerMask.NameToLayer("Ball2"))
        {
            // SUBSTRACT POINTS FROM CLOSEST STUDENT

            _generalBallManager.GetClosestStudent(transform)?.ModifyScore(-1);

        }
        else if(_target.gameObject.layer == LayerMask.NameToLayer("Ball3"))
        {
            // SUBSTRACT LIFE

            _scoreManager.ModifyLives(-1);
        }

        Destroy(_target.gameObject);

        base.FinishAction();
    }

    public override void UpdateState()
    {
        if (_target == null)
        {
            _teacherAI.ChangeState(_goToState);

            return;
        }

        base.UpdateState();
    }
}
