using System;
using UnityEngine;

public class FoundBall : State
{
    [SerializeField] private float _hideBallTime;

    [Header("Settings")]
    [SerializeField] private int _pointsLostUnanswered = -2;
    [SerializeField] private int _pointsLostAnswered = -4;

    private BallController _ballController;

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

        _teacherAI.SetActiveExclamation(true);

        AudioManager.Instance.PlaySound("Alert");
    }
    public override void UpdateState()
    {
        if (_target == null)
        {
            _teacherAI.ChangeState(_goToState);

            return;
        }

        if(_ballController.IsSafe)
        {
            _target = null;

            return;
        }

        base.UpdateState(); 

        // This is to hide the ball mid character animation
        if(_actionCounter > _hideBallTime && _target.gameObject.activeSelf)
        {
            _target.gameObject.SetActive(false);
        }
    }
    public override void ExitState()
    {
        base.ExitState();
        
        _teacherAI.SetActiveExclamation(false);
    }

    public void BallDetected(BallController ballController)
    {
        if (_teacherAI.GetState() is FoundBall)
            return;

        _ballController = ballController;

        _target = ballController.transform;        

        _teacherAI.ChangeState(this);
    }

    public override void FinishAction()
    {
        StudentGrade studentScore = _target.GetComponent<BallController>().Student.GetComponent<StudentGrade>();

        if (_target.gameObject.layer == LayerMask.NameToLayer("Ball2"))
        {
            if (_target.GetComponent<BallController>().ThrownByPlayer)
                studentScore.ModifyScore(_pointsLostAnswered, true);
            else
                studentScore.ModifyScore(_pointsLostUnanswered, true);

        }
        else if(_target.gameObject.layer == LayerMask.NameToLayer("Ball3"))
        {
            // SUBSTRACT LIFE

            if(_target.GetComponent<BallController>().ThrownByPlayer)
                MatchManager.Instance.ModifyLives(-1);
            else
                studentScore.ModifyScore(_pointsLostUnanswered, true);
        }

        Destroy(_target.gameObject);

        base.FinishAction();
    }

    public override void StartActionAnimation()
    {
        base.StartActionAnimation();

        _teacherAI.Animator.SetTrigger("Crouch");
    }
}
