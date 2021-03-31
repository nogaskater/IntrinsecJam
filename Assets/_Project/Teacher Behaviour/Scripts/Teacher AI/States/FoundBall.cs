using System;
using UnityEngine;

public class FoundBall : State
{
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private GeneralBallManager _generalBallManager;

    [SerializeField] private float _hideBallTime;

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

    public override void EnterState()
    {
        base.EnterState();

        if (_target == null)
            return;

        if (_target.position.x < transform.position.x)
        {
            _teacherAI.TeacherAnimation.SetSpriteDirection(Direction.LEFT);
        }
        else
        {
            _teacherAI.TeacherAnimation.SetSpriteDirection(Direction.RIGHT);
        }


        _teacherAI.Animator.SetTrigger("Walk");
    }

    public override void UpdateState()
    {
        if (_target == null)
        {
            _teacherAI.ChangeState(_goToState);

            return;
        }

        base.UpdateState(); 

        if(_actionCounter > _hideBallTime && _target.gameObject.activeSelf)
        {
            _target.gameObject.SetActive(false);
        }
    }

    public override void StartActionAnimation()
    {
        base.StartActionAnimation();

        _teacherAI.Animator.SetTrigger("Crouch");
    }
}
