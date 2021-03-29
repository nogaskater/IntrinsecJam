using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowController : MonoBehaviour
{
    [Header("GameObject References")]
    [SerializeField] private Rigidbody2D _activeBall;

    [Header("Throw Settings")]
    [SerializeField] private float forceMultiplier;
    

    void Start()
    {
        
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

    
}
