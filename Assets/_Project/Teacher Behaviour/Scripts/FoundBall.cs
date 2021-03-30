using UnityEngine;

public class FoundBall : State
{
    public void BallDetected(Transform ballTarget)
    {
        if (_teacherAI.GetState() is FoundBall)
            return;

        _target = ballTarget;

        _teacherAI.ChangeState(this);
    }
}
