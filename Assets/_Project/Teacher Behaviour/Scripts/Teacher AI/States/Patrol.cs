using System;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : State
{
    [SerializeField] private Transform _parentTargets;
    private List<Transform> _targets = new List<Transform>();

    protected override void Awake()
    {
        base.Awake();

        if (_parentTargets == null)
            throw new ArgumentNullException("_parentTargets");

        for (int i = 0; i < _parentTargets.childCount; i++)
        {
            _targets.Add(_parentTargets.GetChild(i));
        }
    }

    public override void EnterState()
    {
        base.EnterState();

        List<Transform> targets = new List<Transform>(_targets);
        targets.Remove(_target);

        int random = UnityEngine.Random.Range(0, targets.Count);

        _target = targets[random];

        if (_target.position.x < transform.position.x)
        {
            _teacherAI.LeftTriggerActive(true);

            _teacherAI.TeacherAnimation.SetSpriteDirection(Direction.LEFT);

        }
        else
        {
            _teacherAI.RightTriggerActive(true);

            _teacherAI.TeacherAnimation.SetSpriteDirection(Direction.RIGHT);
        }

        _teacherAI.Animator.SetTrigger("Walk");
    }

    public override void StartActionAnimation()
    {
        base.StartActionAnimation();

        _teacherAI.Animator.SetTrigger("Idle");
    }

}
