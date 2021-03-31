﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_ThrowController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private GeneralBallManager _generalBallManager;
    [SerializeField] private CharacterAnimation _characterAnimation;

    [Header("GameObject References")]
    //[SerializeField] private Rigidbody2D _npcBall;
    [SerializeField] private Transform _throwStartingPoint;
    [SerializeField] private Transform _player;

    [Header("Throw Settings")]
    [SerializeField] private float _minHeight;
    [SerializeField] private float _maxHeight;

    //--Controlling the assisted throw--//
    private float height;
    private float gravity;
    private float px;
    private float py;
    private float vy;
    private float vx;


    public Transform GetThrowStartingPoint()
    {
        return _throwStartingPoint;
    }
    public Transform GetPlayer()
    {
        return _player;
    }

    private void Awake()
    {
        if (_generalBallManager == null)
            throw new ArgumentNullException("_generalBallManager");
        if (_characterAnimation == null)
            throw new ArgumentNullException("_characterAnimation");
    }

    public void RemoveStudentFromThrowManager()
    {
        _generalBallManager.RemoveStudent(this);
    }

    public void ThrowBall(Rigidbody2D _npcBall)
    {
        height = ComputeRandomHeight();
        gravity = (_npcBall.gravityScale * Physics2D.gravity.magnitude);
        //Debug.Log(gravity);
        vy = Mathf.Sqrt(-2 * -gravity * height);
        px = _player.position.x - _throwStartingPoint.position.x;
        py = _player.position.y - _throwStartingPoint.position.y;
        vx = px / (Mathf.Sqrt(- ((2 * height) / -gravity)) + Mathf.Sqrt((2 * (py - height)) / -gravity));
        //Debug.Log("Vy: " + vy + " /// Vx: " + vx);

        _npcBall.bodyType = RigidbodyType2D.Dynamic;
        _npcBall.velocity = new Vector2(vx, vy);

        _characterAnimation.Animator.SetTrigger("Throw");
    }

    private float ComputeRandomHeight()
    {
        return UnityEngine.Random.Range(_player.position.y + _minHeight, _player.position.y + _maxHeight);
    }
}
