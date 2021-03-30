using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBallTransitionController : MonoBehaviour
{
    [Header("GameObject Referemces")]
    [SerializeField] private GeneralBallManager _gBallManager;
    public void CheckBallAnswers(GameObject ball)
    {
        ball.SetActive(false);

        //gameObject.GetComponent<NPC_ThrowController>().GetPlayer().gameObject.GetComponent<ThrowController>().SetActiveBall(null);

        Debug.Log("Student Received Ball");
        //Kaneri guapetón te toca
        //Check if the answeris right or not
        //--TO-DO--//


        _gBallManager.RemoveCurrentBallFromController(ball.GetComponent<Rigidbody2D>());
    }
}
