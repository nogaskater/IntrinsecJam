using System;
using UnityEngine;

public class StudentAI : MonoBehaviour
{
    [SerializeField] private StudentScore _studentScore;

    [SerializeField] private Transform _exitRoomTarget;

    [SerializeField] private float _movementSpeed = 3;


    private bool _hasFinished = false;
    private void Awake()
    {
        if (_studentScore == null)
            throw new ArgumentNullException("_studentScore");

        _studentScore.OnStudentFinished += StudentFinished;

        if (_exitRoomTarget == null)
            throw new ArgumentNullException("_target");
    }

    private void Update()
    {
        if(_hasFinished)
        {
            Vector2 direction = MyUtilities.DirectionVector(transform.position, _exitRoomTarget.position).normalized;

            transform.Translate(direction * _movementSpeed * Time.deltaTime);
        }
    }

    private void StudentFinished(bool aproved)
    {
        _hasFinished = true;
    }
}
