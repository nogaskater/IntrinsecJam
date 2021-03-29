﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D _rb;

    [Header("Ball Settings")]
    [SerializeField] private float _gravityFactor;
    [SerializeField] private int _maxBounces;

    //--Controlling bouncing times--//
    public int contCollisions = 0;

    //--Controlling original gravity scale--//
    private float originalGScale;

    //--Controlling falling process--//
    private bool isfalling;
    private int contFalling = 0;

    void Start()
    {
        originalGScale = _rb.gravityScale;
    }
    void FixedUpdate()
    {
        CheckIsFalling();
        CheckCollisionLimit();
    }

    private void CheckIsFalling()
    {
        if(_rb.velocity.y < 0.0f)
        {
            isfalling = true;
        }
        else
        {
            isfalling = false;
        }

        _rb.gravityScale = ComputeIncreasedGravity();
    }

    private float ComputeIncreasedGravity()
    {
        if(isfalling && contFalling <1)
        {
            contFalling++;
            return _rb.gravityScale * _gravityFactor;
        }
        else
        {
            contFalling = 0;
            return originalGScale;
        }
    }

    private void CheckCollisionLimit()
    {
        if (contCollisions > _maxBounces)
        {
            Debug.Log("Collision limit reached");
            StopMovement();
        }
    }

    private void StopMovement()
    {
        _rb.velocity = Vector2.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        contCollisions++;
    }
}
