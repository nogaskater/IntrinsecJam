using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBallTransitionController : MonoBehaviour
{
    [Header("GameObject Referemces")]
    [SerializeField] private ThrowController _throwController;
    [SerializeField] private CharacterAnimation _characterAnimation;
    [SerializeField] private TableBehaviour _table;

    private void Awake()
    {
        if (_throwController == null)
            throw new ArgumentNullException("_throwController");
        if (_characterAnimation == null)
            throw new ArgumentNullException("_characterAnimation");
        if (_table == null)
            throw new ArgumentNullException("_table");
    }

    public void PutBallInBox(GameObject ball)
    {

        _characterAnimation.Animator.SetTrigger("Catch");

        AudioManager.Instance.PlaySound("Catch");

        ball.SetActive(false);

        _table.AddUnansweredPaper(ball.GetComponent<BallController>());
    }

    public void PutBallInHand(BallController ball)
    {
        ball.gameObject.SetActive(true);
        _throwController.SetActiveBall(ball.GetComponent<Rigidbody2D>()); 
        _throwController.ResetBall();

        ball.Student.HolderActive(true);

    }
    public void RemoveBallFromHand(BallController ball)
    {
        ball.gameObject.SetActive(false);
        ball.Student.HolderActive(false);
    }

    public Rigidbody2D GetCurrentBall()
    {
        return _throwController.GetActiveBall();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_table.IsThereRoomForMoreNewPapers())
            return;

        if (collision.gameObject.tag == "Ball")
        {
            if (collision.gameObject.GetComponent<BallController>().Answer == ExamElement.NONE)
            {
                PutBallInBox(collision.gameObject);
            }
        }
    }
}
