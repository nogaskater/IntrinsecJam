using System;
using UnityEngine;

public class TeacherBallDetection : MonoBehaviour
{
    [SerializeField] private TeacherAI _teacherAI;

    [SerializeField] private HitByBall _hitByBall;


    private void Awake()
    {
        if (_teacherAI == null)
            throw new ArgumentNullException("_teacherAI");
        if (_hitByBall == null)
            throw new ArgumentNullException("_hitByBall");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_teacherAI.CurrentState is HitByBall)
            return;

        if (_teacherAI.CurrentState is FoundBall)
            return;

        if (collision.gameObject.tag == "Ball")
        {
            BallController ballCOntroller = collision.transform.GetComponent<BallController>();

            if (ballCOntroller == null)
                throw new InvalidOperationException(collision.gameObject + " has a 'Ball' tag but has no BallController script attached.");
            

            _hitByBall.Hit(ballCOntroller);

            collision.gameObject.layer = LayerMask.NameToLayer("Ball3");
        }
    }
}


