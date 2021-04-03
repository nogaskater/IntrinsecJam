using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBallTrigger : MonoBehaviour
{
    [SerializeField] private Student _student;
    [SerializeField] private StudentGrade _studentGrade;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            BallController ballController = collision.gameObject.GetComponent<BallController>();

            if (ballController.Student == _student && ballController.Answer != ExamElement.NONE)
            {
                _studentGrade.CheckBallAnswers(ballController);
            }
        }
    }
}
