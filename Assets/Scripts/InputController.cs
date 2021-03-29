using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{

    [Header("GameObject References")]
    [SerializeField] private ThrowController _throwController;
    [SerializeField] private NPC_ThrowController _NPCthrowController;      //Debug-Only

    [Header("Input Settings")]
    [SerializeField] private float _holdInterval;
    [SerializeField] private float _deathZoneRadius;
    [SerializeField] private float _maxDragRadius;

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
        CheckResetBall();
        CheckNPCThrowBall();
    }

    public void CheckLeftClick()        //Remains To-Do the raycast part to the player. to know if its a fire action or another thing
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

                if(((Vector2)Input.mousePosition-startClickPos).magnitude < _maxDragRadius)
                    _throwController.UpdateArrowUI((new Vector2(Input.mousePosition.x, Input.mousePosition.y) - startClickPos) * -1.0f, (new Vector2(Input.mousePosition.x, Input.mousePosition.y) - startClickPos).magnitude, true, _maxDragRadius);
                else
                {
                    //Debug.Log("Outside Max Drag Radius");
                    _throwController.UpdateArrowUI((new Vector2(Input.mousePosition.x, Input.mousePosition.y) - startClickPos) * -1.0f, (new Vector2(Input.mousePosition.x, Input.mousePosition.y) - startClickPos).magnitude, false, _maxDragRadius);
                }
            }

        }
        else
        {
            endClickPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            _throwController.DisableArrow();

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

    public void CheckResetBall()
    {
        if(Input.GetButtonDown("Reset Ball"))
        {
            _throwController.ResetBall();
        }
    }

    public void CheckNPCThrowBall()         //Debug-Only
    {
        if(Input.GetButtonDown("NPC Throw Ball"))
        {
            _NPCthrowController.ThrowBall();
        }
    }

    private void CheckDeathZone()
    {
        Vector2 inputDir = endClickPos - startClickPos;
        float inputMag = (endClickPos - startClickPos).magnitude;

        //Debug.Log(inputMag);

        if (inputMag >= _deathZoneRadius)
        {
            if(inputMag > _maxDragRadius)
                _throwController.ThrowBall(inputDir, _maxDragRadius, _deathZoneRadius, _maxDragRadius);
            else
                _throwController.ThrowBall(inputDir, inputMag, _deathZoneRadius, _maxDragRadius);
        }
    }



}
