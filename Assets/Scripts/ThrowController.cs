using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowController : MonoBehaviour
{
    [Header("GameObject References")]
    [SerializeField] private Rigidbody2D _activeBall;
    [SerializeField] private Transform _throwStartingPoint;

    [Header("UI References")]
    [SerializeField] private GameObject _arrow;
    //[SerializeField] private GameObject _deadZone;

    [Header("UI Settings")]
    [SerializeField] private float _minXScaleArrow;
    [SerializeField] private float _maxXScaleArrow;
    [SerializeField] private float _ballOffsetArrow;

    [Header("Throw Settings")]
    [SerializeField] private float forceMultiplier;
    

    void Start()
    {
        DisableArrow();
    }
    void Update()
    {
        
    }

    public void ThrowBall(Vector2 initialDirection, float initialMagnitude)
    {
        Vector2 ballForce = ComputeInitialForce(initialDirection, initialMagnitude);
        _activeBall.bodyType = RigidbodyType2D.Dynamic;
        _activeBall.AddForce(ballForce, ForceMode2D.Impulse);
    }

    private Vector2 ComputeInitialForce(Vector2 initialDirection, float initialMagnitude)
    {
        return (initialDirection.normalized * -1.0f) * initialMagnitude * forceMultiplier;
    }

    public void ResetBall()        //Debug-Only
    {
        _activeBall.transform.position = _throwStartingPoint.position;
        _activeBall.velocity = Vector2.zero;
        _activeBall.angularVelocity = 0.0f;
        _activeBall.bodyType = RigidbodyType2D.Kinematic;
    }

    public void UpdateArrowUI(Vector2 initialDirection, float initialMagnitude)
    {
        _arrow.SetActive(true);  
        _arrow.transform.position = _throwStartingPoint.position + (Vector3)initialDirection.normalized * _ballOffsetArrow;            
        _arrow.transform.localRotation = Quaternion.AngleAxis(Vector2.SignedAngle(new Vector2(1.0f, 0.0f), initialDirection.normalized), new Vector3(0.0f, 0.0f, 1.0f));

        float newScale = ReMap(initialMagnitude, 0.0f, 1000.0f, _minXScaleArrow, _maxXScaleArrow);

        _arrow.transform.localScale = new Vector3(newScale, _arrow.transform.localScale.y, _arrow.transform.localScale.z);
    }

    public void DisableArrow()
    {
        _arrow.SetActive(false);
    }

    private float ReMap(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }


}
