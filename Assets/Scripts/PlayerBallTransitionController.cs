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
        ball.SetActive(false);

        Debug.Log("Ball to the box");

        table.AddNewPaper(ball.GetComponent<BallController>()._ballPaper);
        //Sergio cariño te toca
        //Create a new paper in the box with the data from the ball(paper)
        //--TO-DO--//

    }

    public void PutBallInHand(GameObject ball)
    {
        //Sergio calienta que sales
        //Remove selected paper from the box, passing its data to the ball(paper)
        //--TO-DO--//
        Debug.Log("Ball to the hand");

        ball.SetActive(true);
        _throwController.SetActiveBall(ball.GetComponent<Rigidbody2D>());       //Tener en cuenta que pasa si ya tienes una bola  en la mano!!!!
        _throwController.ResetBall();

    }
}
