using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum CurrentObject { NONE, PLAYER, BALL, NPC };
public class InputController : MonoBehaviour
{
    [Header("GameObject References")]
    [SerializeField] private ThrowController _throwController;
    [SerializeField] private GameObject _ballPrefab;      

    [Header("Input Settings")]
    [SerializeField] private float _holdInterval;
    [SerializeField] private float _deathZoneRadius;
    [SerializeField] private float _maxDragRadius;

    //--Controlling drag times--//
    public float prevClickTime;

    //--Controlling distance between drags--// 
    private Vector2 startClickPos;
    private Vector2 endClickPos;

    //--Controlling selected object--//
    private CurrentObject currentObject;
    private CurrentObject lastClickedObj;

    private void Awake()
    {
        if (_throwController == null)
            throw new ArgumentNullException("_throwController");
    }

    void Start()
    {
        
    }
    void Update()
    {
        CheckLeftClick();
        CheckEscClick();
        //CheckResetBall();

        //CheckSpawnBallPlayer();
    }

    public void CheckLeftClick()        //Remains To-Do the raycast part to the player. to know if its a fire action or another thing
    {
        SetCurrentObject();


        if (Input.GetButtonDown("Fire1"))
        {
            prevClickTime = Time.time;
            startClickPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            lastClickedObj = currentObject;

            // AEUGH
            if (lastClickedObj != CurrentObject.PLAYER)
                return;

            if(_throwController.GetActiveBall() != null)
                _throwController.TriggerChargeAnimation();

        }
        else if(Input.GetButton("Fire1"))
        {

            if (lastClickedObj != CurrentObject.PLAYER)
                return;


            if (Time.time - prevClickTime >= _holdInterval)
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
            lastClickedObj = CurrentObject.NONE;

        }
    }

    private void SetCurrentObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

        if (hit.collider != null)
        {
            //Debug.Log(hit.transform.tag);
            switch (hit.transform.tag)
            {
                case "Player":
                    currentObject = CurrentObject.PLAYER;
                    break;
                case "Ball":
                    currentObject = CurrentObject.BALL;
                    break;
                case "NPC":
                    currentObject = CurrentObject.NPC;
                    break;
                default:
                    currentObject = CurrentObject.NONE;
                    break;
            }
        }
    }

    public void CheckEscClick()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            Debug.Log("Pause Button - CLICKED");
        }
    }

    //public void CheckResetBall()
    //{
    //    if(Input.GetButtonDown("Reset Ball"))
    //    {
    //        _throwController.ResetBall();
    //    }
    //}

    //public void CheckSpawnBallPlayer()      //Debug-Only
    //{
    //    if (Input.GetButtonDown("Spawn Ball Player") /*&& _throwController.GetActiveBall()!=null*/)
    //    {
    //        GameObject pBall = Instantiate(_ballPrefab, _throwController.GetThrowStartingPoint().position, Quaternion.identity);
    //        pBall.GetComponent<BallController>().BallPaper.student_ID = 1;
    //        pBall.GetComponent<BallController>().BallPaper.answer = ExamElement.EXAM_ELEMENT_10;
    //        _throwController.SetActiveBall(pBall.GetComponent<Rigidbody2D>());

    //    }
    //}

    private void CheckDeathZone()
    {
        Vector2 inputDir = endClickPos - startClickPos;
        float inputMag = (endClickPos - startClickPos).magnitude;

        //Debug.Log(inputMag);

        if (inputMag >= _deathZoneRadius && lastClickedObj == CurrentObject.PLAYER)
        {
            if(inputMag > _maxDragRadius)
                _throwController.ThrowBall(inputDir, _maxDragRadius, _deathZoneRadius, _maxDragRadius);
            else
                _throwController.ThrowBall(inputDir, inputMag, _deathZoneRadius, _maxDragRadius);
        }
    }
}
