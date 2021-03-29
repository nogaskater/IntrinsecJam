﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{

    [Header("GameObject References")]
    [SerializeField] private ThrowController _throwController;

    [Header("Input Settings")]
    [SerializeField] private float _holdInterval;
    [SerializeField] private float _deathZoneRadius;

    //--Controlling drag times--//
    public float prevClickTime;

    //--Controlling distance between drags--// 
    private Vector2 startClickPos;
    private Vector2 endClickPos;

    void Start()
    {
        
    }
    void Update()
    {
        CheckLeftClick();
        CheckEscClick();
    }

    public void CheckLeftClick()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            prevClickTime = Time.time;
            startClickPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            //Debug.Log("Left Click - CLICKED");
        }
        else if(Input.GetButton("Fire1"))
        {
            
            if(Time.time - prevClickTime >= _holdInterval)
            {
                //Debug.Log("Left Click - DRAGGING");
            }

        }
        else
        {
            endClickPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            if (prevClickTime > 0.0f)
                CheckDeathZone();

            prevClickTime = 0.0f;

        }
    }

    public void CheckEscClick()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            Debug.Log("Pause Button - CLICKED");
        }
    }

    private void CheckDeathZone()
    {
        Vector2 inputDir = endClickPos - startClickPos;
        float inputMag = (endClickPos - startClickPos).magnitude;

        Debug.Log(ReMap(inputMag, 0, 1000, 0, 1));

        if (inputMag >= _deathZoneRadius)
        {
            _throwController.ThrowBall(inputDir, inputMag);
        }
    }

    private float ReMap(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }
}
