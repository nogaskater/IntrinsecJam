using System;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : State
{
    [SerializeField] private Transform _waypointParent;
    private List<Transform> _targets = new List<Transform>();

    protected override void Awake()
    {
        base.Awake();

        if (_waypointParent == null)
            throw new ArgumentNullException("_parentTargets");

        if (_waypointParent.childCount == 0)
            throw new InvalidOperationException("There are no waypoints set as child of the waypoint parent.");

        for (int i = 0; i < _waypointParent.childCount; i++)
        {
            Transform waypoint = _waypointParent.GetChild(i);

            if(waypoint.gameObject.activeSelf)
                _targets.Add(waypoint);
        }
    }

    public override void EnterState()
    {
        base.EnterState();

        List<Transform> targets = new List<Transform>(_targets);

        if(targets.Count == 1)
        {
            _target = targets[0];
        }
        else
        {
            targets.Remove(_target);

            int random = UnityEngine.Random.Range(0, targets.Count);

            _target = targets[random];
        }

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
