using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBallTrigger : MonoBehaviour
{
    [SerializeField] private PlayerBallTransitionController _ballTransitionController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            if(collision.gameObject.GetComponent<BallController>().BallPaper.answer == ExamElement.NONE)
            {
                _ballTransitionController.PutBallInBox(collision.gameObject);
            }
        }
    }
}
