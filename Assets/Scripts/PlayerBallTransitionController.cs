using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBallTransitionController : MonoBehaviour
{
    [Header("GameObject Referemces")]
    [SerializeField] private ThrowController _throwController;
    [SerializeField] private TableBehaviour _table;
    [SerializeField] private CharacterAnimation _characterAnimation;

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
        if (_table.ToAnserQueueCount >= _table.MaxListSize)
            return;

        _characterAnimation.Animator.SetTrigger("Catch");

        AudioManager.Instance.PlaySound("Catch");

        ball.SetActive(false);

        _table.AddNewPaper(ball.GetComponent<BallController>().BallPaper);
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
