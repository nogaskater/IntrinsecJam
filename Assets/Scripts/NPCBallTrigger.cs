using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBallTrigger : MonoBehaviour
{
    [SerializeField] private NPCBallTransitionController _ballTransitionController;
    [SerializeField] private Student _student;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            BallController ballController = collision.gameObject.GetComponent<BallController>();

            if (ballController.Student == _student && ballController.BallPaper.answer != ExamElement.NONE)
            {
                _ballTransitionController.CheckBallAnswers(collision.gameObject);
            }
        }
    }
}
