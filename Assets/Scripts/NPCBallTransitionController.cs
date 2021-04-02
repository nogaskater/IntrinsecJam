using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBallTransitionController : MonoBehaviour
{
    [Header("GameObject References")]
    [SerializeField] private GeneralBallManager _gBallManager;
    [SerializeField] private StudentScore _studentScore;

    [SerializeField] private CharacterAnimation _characterAnimation;


    private void Awake()
    {
        if (_gBallManager == null)
            throw new ArgumentNullException("_gBallManager");
        if (_studentScore == null)
            throw new ArgumentNullException("_studentScore");

        if (_characterAnimation == null)
            throw new ArgumentNullException("_characterAnimation");
    }

    public Action<int, bool> OnBallReceived; 
    public void CheckBallAnswers(GameObject ball)
    {
        if (_studentScore.Grade <= 0 || _studentScore.Grade >= 10)
            return;


        BallController paper = ball.GetComponent<BallController>();

        if(paper.Answer == paper.Question)        
            OnBallReceived?.Invoke(2, true);
        else
            OnBallReceived?.Invoke(-2, true);


        if (_studentScore.Grade > 0 || _studentScore.Grade < 10)
            AudioManager.Instance.PlaySound("Catch");


        _gBallManager.RemoveCurrentBallFromController(ball.GetComponent<Rigidbody2D>());

        Destroy(ball.gameObject);
    }
}
