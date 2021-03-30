using System;
using UnityEngine;

public class BallDetection : MonoBehaviour
{
    [SerializeField] private FoundBall _foundBallState;

    private void Awake()
    {
        if (_foundBallState == null)
            throw new ArgumentNullException("_foundBallState");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            _foundBallState.BallDetected(collision.transform);

            collision.gameObject.layer = LayerMask.NameToLayer("Ball2");
        }
    }
}
