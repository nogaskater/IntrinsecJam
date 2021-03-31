using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TeacherAI : MonoBehaviour
{
    [SerializeField] private State _startingState;
    [SerializeField] private CharacterAnimation _teacherAnimation;
    public Animator Animator => _teacherAnimation.Animator;
    public CharacterAnimation TeacherAnimation => _teacherAnimation;



    public State CurrentState { get; private set; }
    public State GetState() => CurrentState;


    [Header("Triggers")]
    [SerializeField] private GameObject _rightTrigger;
    [SerializeField] private GameObject _leftTrigger;

    private void Awake()
    {
        if (_startingState == null)
            throw new ArgumentNullException("_startingState");
        if (_teacherAnimation == null)
            throw new ArgumentNullException("_teacherAnimation");


        if (_rightTrigger == null)
            throw new ArgumentNullException("_rightTrigger");
        if (_leftTrigger == null)
            throw new ArgumentNullException("_leftTrigger");
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
        toState.EnterState();

        CurrentState = toState;
    }


    public void RightTriggerActive(bool active)
    {
        _rightTrigger.SetActive(active);
    }
    public void LeftTriggerActive(bool active)
    {
        _leftTrigger.SetActive(active);
    }

}

