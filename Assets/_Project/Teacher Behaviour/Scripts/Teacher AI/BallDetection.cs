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
            BallController ballController = collision.GetComponent<BallController>();

            if (ballController == null)
                throw new InvalidOperationException("No BallController script is attached to a gameobject that has a Ball tag.");

            _foundBallState.BallDetected(ballController);

            collision.gameObject.layer = LayerMask.NameToLayer("Ball2");
        }
    }
}
