using System;
using UnityEngine;

public class CheckStudent : State
{
    public void CalledByStudent(Transform target)
    {
        if (_teacherAI.GetState() is CheckStudent)
            return;

        _target = target;

        _teacherAI.ChangeState(this);
    }

    public override void EnterState()
    {
        base.EnterState();

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

    public override void StartActionAnimation()
    {
        base.StartActionAnimation();

        _teacherAI.Animator.SetTrigger("StudentCall");

        _teacherAI.TeacherAnimation.SetSpriteDirection(Direction.LEFT);
    }
}
