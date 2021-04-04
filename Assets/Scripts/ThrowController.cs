using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowController : MonoBehaviour
{
    [SerializeField] private CharacterAnimation _characterAnimation;

    [Header("GameObject References")]
    [SerializeField] private Transform _throwStartingPoint;
    [SerializeField] private LineRenderer _tracerLine;

    [Header("UI References")]
    [SerializeField] private GameObject _arrow;

    [Header("UI Settings")]
    [SerializeField] private float _minXScaleArrow;
    [SerializeField] private float _maxXScaleArrow;
    [SerializeField] private float _ballOffsetArrow;

    [Header("Throw Settings")]
    [SerializeField] private float forceMultiplier;
    [SerializeField] private float _timeBetweenTraces;



    private Rigidbody2D _activeBall;


    public Action OnBallThrow;


    public Rigidbody2D GetActiveBall()
    {
        return _activeBall;
    }
    public void RemoveActiveBall()
    {
        _activeBall = null;
    }
    public void SetActiveBall(Rigidbody2D rb)
    {
        _activeBall = rb;
    }
    public Transform GetThrowStartingPoint()
    {
        return _throwStartingPoint;
    }

    private void Awake()
    {
        if (_characterAnimation == null)
            throw new ArgumentNullException("_characterAnimation");
    }

    void Start()
    {
        DisableArrow();
    }

    public void TriggerChargeAnimation()
    {
        _characterAnimation.Animator.SetTrigger("Charge");
    }

    public void ThrowBall(Vector2 initialDirection, float initialMagnitude, float minRadPossible, float maxRadPossible)
    {
        if(_activeBall!=null)
        {
            Vector2 ballForce = ComputeInitialForce(initialDirection, initialMagnitude, minRadPossible, maxRadPossible);
            _activeBall.bodyType = RigidbodyType2D.Dynamic;
            _activeBall.AddForce(ballForce, ForceMode2D.Impulse);

            //Debug.Log("Initial Force: " + ballForce);
            //Debug.Log("Initial Velocity: " + CalculateIntialVelocity(ballForce, _activeBall.mass));

            _activeBall.gameObject.layer = LayerMask.NameToLayer("Ball");

            BallController ballController = _activeBall.GetComponent<BallController>();

            ballController.Student.HolderActive(false);
            ballController.ThrownByPlayer = true;
            ballController.IsSafe = false;

            _activeBall = null;

            _characterAnimation.Animator.SetTrigger("Throw");

            AudioManager.Instance.PlayRandomThrow();

            OnBallThrow?.Invoke();
        }
    }

    private Vector2 ComputeInitialForce(Vector2 initialDirection, float initialMagnitude, float minRadPossible, float maxRadPossible)
    {
        //Debug.Log("Magnitude raw: " + initialMagnitude + " //// Magnitude normalized: " + ReMap(initialMagnitude, 40.0f, 250.0f, 0.01f, 1.0f));
        return (initialDirection.normalized * -1.0f) * ReMap(initialMagnitude, minRadPossible, maxRadPossible, 0.01f, 1.0f) * forceMultiplier;
    }

    public void ResetBall()        
    {
        _activeBall.transform.position = _throwStartingPoint.position;
        _activeBall.velocity = Vector2.zero;
        _activeBall.angularVelocity = 0.0f;
        _activeBall.bodyType = RigidbodyType2D.Kinematic;
    }

    public void UpdateArrowUI(Vector2 initialDirection, float initialMagnitude, bool inMaxRadius, float maxRadPossible)
    {
        float newScale = 0.0f;
        float colorValue = 0.0f;

        float dragMag = 0.0f;

        if (_activeBall!=null)
        {
            _arrow.SetActive(true);
            _arrow.transform.position = _throwStartingPoint.position + (Vector3)initialDirection.normalized * _ballOffsetArrow;
            _arrow.transform.localRotation = Quaternion.AngleAxis(Vector2.SignedAngle(new Vector2(1.0f, 0.0f), initialDirection.normalized), new Vector3(0.0f, 0.0f, 1.0f));

            colorValue = ReMap(initialMagnitude, 0.0f, maxRadPossible, 0.0f, 1.0f);

            dragMag = maxRadPossible;

            if (inMaxRadius)
            {
                newScale = ReMap(initialMagnitude, 0.0f, maxRadPossible, _minXScaleArrow, _maxXScaleArrow);
                _arrow.transform.localScale = new Vector3(newScale, _arrow.transform.localScale.y, _arrow.transform.localScale.z);

                colorValue = ReMap(newScale, _minXScaleArrow, _maxXScaleArrow, 0.0f, 1.0f);

                dragMag = initialMagnitude;
            }

            DrawTrace(CalculateIntialVelocity(ComputeInitialForce(initialDirection * -1.0f, dragMag, 40.0f, 250.0f), _activeBall.mass));
            //Debug.Log("Force computed: " + ComputeInitialForce(initialDirection * -1.0f, dragMag, 40.0f, 250.0f));
            ChangeArrowColorGradient(colorValue);
        }
    }

    private void ChangeArrowColorGradient(float value)
    {
        Color c;

        if(value <= 0.5f)
        {
            c = new Color(value * 2.0f, 1.0f, 0.0f);
        }
        else
        {
            c = new Color(1.0f , 1.0f - (value / 2.0f), 0.0f);
        }

        _arrow.transform.GetChild(0).GetComponent<Image>().color = c;
        _tracerLine.material.color = c;
    }

    public void DisableArrow()
    {
        _arrow.SetActive(false);
        DisableTrace();
    }

    private void DrawTrace(Vector2 v0)
    {
        GetComponent<LineRenderer>().enabled = true;

        for (int i = 0; i <_tracerLine.positionCount; i++)
        {
            _tracerLine.SetPosition(i, (Vector3) CalculatePosInTime(v0, i * _timeBetweenTraces));
        }
    }

    private void DisableTrace()
    {

        GetComponent<LineRenderer>().enabled = false;

    }

    private Vector2 CalculateIntialVelocity(Vector2 force, float mass)
    {
        return (force / mass) * 1.0f/*Time.fixedDeltaTime*/;
    }

    private Vector3 CalculatePosInTime(Vector2 v0, float t)
    {
        Vector3 result = _throwStartingPoint.position;

        result.x = result.x + v0.x * t;
        result.y = (-0.5f * Mathf.Abs(Physics2D.gravity.y * _activeBall.gravityScale) * (t * t)) + (v0.y * t) + result.y;

        //Debug.Log("Time: " + t + " /// Position: " + result);

        return result;
    }

    private float ReMap(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }


}
