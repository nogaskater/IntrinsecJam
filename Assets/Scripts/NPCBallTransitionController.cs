using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBallTransitionController : MonoBehaviour
{
    [Header("GameObject References")]
    [SerializeField] private GeneralBallManager _gBallManager;
    [SerializeField] private StudentScore _studentScore;


    private void Awake()
    {
        if (_gBallManager == null)
            throw new ArgumentNullException("_gBallManager");
        if (_studentScore == null)
            throw new ArgumentNullException("_studentScore");

    }

    public Action<int, bool> OnBallReceived; 
    public void CheckBallAnswers(GameObject ball)
    {
        if (_studentScore.Grade <= 0)
            return;
        
        OnBallReceived?.Invoke(2, true);
        //Check if the answer is right or not
        Debug.LogWarning("FALTA POR IMPLEMENTAR");
        

        _gBallManager.RemoveCurrentBallFromController(ball.GetComponent<Rigidbody2D>());

        Destroy(ball.gameObject);
    }
}
