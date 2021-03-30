using System;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : State
{
    [SerializeField] private Transform _parentTargets;
    private List<Transform> _targets = new List<Transform>();

    [SerializeField] private GameObject _rightTrigger;
    [SerializeField] private GameObject _leftTrigger;


    private void Awake()
    {
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

        _rightTrigger.SetActive(false);
        _leftTrigger.SetActive(false);

        List<Transform> targets = new List<Transform>(_targets);
        targets.Remove(_target);

        int random = UnityEngine.Random.Range(0, targets.Count);

        _target = targets[random];

        if (_target.position.x < transform.position.x)
        {
            _leftTrigger.SetActive(true);
        }
        else
        {
            _rightTrigger.SetActive(true);
        }
    }

}
