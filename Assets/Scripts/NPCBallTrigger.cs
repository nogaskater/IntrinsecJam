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
            if (collision.gameObject.GetComponent<BallController>().Student == _student)
            {
                _ballTransitionController.CheckBallAnswers(collision.gameObject);
            }
        }
    }
}
