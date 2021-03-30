using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TeacherAI : MonoBehaviour
{
    [SerializeField] private State _startingState;

    public State CurrentState { get; private set; }
    public State GetState() => CurrentState;

    private void Awake()
    {
        if (_startingState == null)
            throw new ArgumentNullException("_startingState");
    }

    private void Start()
    {
        ChangeState(_startingState);
    }
    private void Update()
    {
        CurrentState.UpdateState();
    }

    public void ChangeState(State toState)
    {
        CurrentState = toState;

        CurrentState.EnterState();
    }
}
