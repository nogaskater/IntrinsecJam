using System;
using UnityEngine;

public class TeacherBallDetection : MonoBehaviour
{
    [SerializeField] private HitByBall _hitByBall;

    private void Awake()
    {
        if (_hitByBall == null)
            throw new ArgumentNullException("_hitByBall");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Ball")
        {
            _hitByBall.Hit(collision.transform);

            collision.gameObject.layer = LayerMask.NameToLayer("Ball2");
        }
    }
}


