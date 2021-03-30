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
}
