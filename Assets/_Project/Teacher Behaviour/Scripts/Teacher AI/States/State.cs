using System;
using UnityEngine;

public class State : MonoBehaviour
{
    [SerializeField] protected TeacherAI _teacherAI;

    [Header("Settings")]
    [SerializeField] private float _movementSpeed = 2;
    [SerializeField] private float _stopDistance = 0.1f;
    [SerializeField] private float _actionTime = 3.0f;

    [Header("Go To State")]
    [SerializeField] protected State _goToState;

    protected Transform _target;

    protected bool _isNearTarget = false;
    protected float _actionCounter = 0;

    private bool _walkActionDone = false;

    protected virtual void Awake()
    {
        if (_teacherAI == null)
            throw new ArgumentNullException("_teacherAI");
        if (_goToState == null)
            throw new ArgumentNullException("_goToState");
    }

    public virtual void EnterState()
    {
        _isNearTarget = false;
        _actionCounter = 0;
        _walkActionDone = false;

        _teacherAI.RightTriggerActive(false);
        _teacherAI.LeftTriggerActive(false);

    }
    public virtual void ExitState()
    {

    }
    public virtual void UpdateState()
    {
        Vector2 direction = MyUtilities.DirectionVector(transform.position, _target.position);

        if (!_isNearTarget)
        {
            if(!_walkActionDone)
            {
                WalkAction();
                _walkActionDone = true;
            }

            transform.Translate(direction.normalized * _movementSpeed * Time.deltaTime);

            if (direction.magnitude < _stopDistance)
            {
                _isNearTarget = true;
            }
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

    public virtual void FinishAction()
    {
        _teacherAI.ChangeState(_goToState);
    }
    public virtual void WalkAction()
    {

    }

}
