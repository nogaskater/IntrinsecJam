using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBallTransitionController : MonoBehaviour
{
    [Header("GameObject Referemces")]
    [SerializeField] private ThrowController _throwController;
    [SerializeField] TableBehaviour table;

    public void PutBallInBox(GameObject ball)
    {
        if (table.ToAnserQueueCount >= table.MaxListSize)
            return;

        ball.SetActive(false);

        table.AddNewPaper(ball.GetComponent<BallController>().BallPaper);
    }

    public void PutBallInHand(GameObject ball)
    {
        ball.SetActive(true);
        _throwController.SetActiveBall(ball.GetComponent<Rigidbody2D>()); 
        _throwController.ResetBall();

        ball.GetComponent<BallController>().Student.HolderActive(true);

    }

    public Rigidbody2D GetCurrentBall()
    {
        return _throwController.GetActiveBall();
    }
}
