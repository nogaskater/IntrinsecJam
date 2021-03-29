using System.Collections;
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
        CheckResetBall();
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

                _throwController.UpdateArrowUI((new Vector2(Input.mousePosition.x, Input.mousePosition.y) - startClickPos) * -1.0f, (new Vector2(Input.mousePosition.x, Input.mousePosition.y) - startClickPos).magnitude);
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

    private void CheckDeathZone()
    {
        Vector2 inputDir = endClickPos - startClickPos;
        float inputMag = (endClickPos - startClickPos).magnitude;

        //Debug.Log(inputMag);

        if (inputMag >= _deathZoneRadius)
        {
            _throwController.ThrowBall(inputDir, inputMag);
        }
    }


}
