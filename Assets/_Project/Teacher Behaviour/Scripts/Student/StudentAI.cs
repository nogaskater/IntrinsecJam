using System;
using System.Collections.Generic;
using UnityEngine;

public class StudentAI : MonoBehaviour
{
    [SerializeField] private StudentGrade _studentScore;
    [SerializeField] private float _movementSpeed = 3;
    [SerializeField] private CharacterAnimation _characterAnimation;
    [SerializeField] private Transform _scoreUITransform;
    [SerializeField] private List<SpriteRenderer> _sprites;


    private Transform _exitRoomTarget;


    [Header("Start Moving Settings")]
    [SerializeField] private float _startMovingTime;

    private float _startMovingCounter = 0;
    private bool _hasStartedMoving = false;

    public bool HasFinished { get; private set; }


    public void Initialize(Transform exitTransform)
    {
        _exitRoomTarget = exitTransform;
    }

    private void Awake()
    {
        if (_studentScore == null)
            throw new ArgumentNullException("_studentScore");
        if (_characterAnimation == null)
            throw new ArgumentNullException("_characterAnimation");

        _studentScore.OnStudentFinished += StudentFinished;

        _startMovingCounter = 0;


        if (_scoreUITransform == null)
            throw new ArgumentNullException("_scoreUITransform");
    }

    private void Update()
    {
        if(HasFinished)
        {
            if (!_hasStartedMoving)
            {
                _startMovingCounter += Time.deltaTime;

                Vector3 direction = MyUtilities.DirectionVectorZ(transform.position, _exitRoomTarget.position);

                transform.Translate(direction * _movementSpeed * Time.deltaTime);

                if (_startMovingCounter > _startMovingTime)
                {
                    _hasStartedMoving = true;

                    _characterAnimation.SetSpriteDirection(Direction.LEFT);
                }
            }
            else
            {
                Vector2 direction = MyUtilities.DirectionVectorX(transform.position, _exitRoomTarget.position).normalized;

                transform.Translate(direction * _movementSpeed * Time.deltaTime);
            }
        }
    }

    public void FinishStudent()
    {
        StudentFinished(_studentScore.Grade >= 5);
    }

    private void StudentFinished(bool passed)
    {
        HasFinished = true;

        if (passed)
        {
            _movementSpeed *= 1.7f;

            _characterAnimation.Animator.SetTrigger("Passed");
        }
        else
            _characterAnimation.Animator.SetTrigger("Failed");

        foreach (var sprite in _sprites)
        {
            sprite.sortingOrder += 10;
        }

        _scoreUITransform.SetParent(null);
    }
}
