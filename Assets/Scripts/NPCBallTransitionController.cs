using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBallTransitionController : MonoBehaviour
{
    [Header("GameObject References")]
    [SerializeField] private GeneralBallManager _gBallManager;

    public Action<int> OnBallReceived; 
    public void CheckBallAnswers(GameObject ball)
    {
        Debug.Log("Student Received Ball");


        
        OnBallReceived?.Invoke(1);
        //Check if the answer is right or not



        _gBallManager.RemoveCurrentBallFromController(ball.GetComponent<Rigidbody2D>());

        Destroy(ball.gameObject);
    }
}
