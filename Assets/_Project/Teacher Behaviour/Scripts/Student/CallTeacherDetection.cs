using System;
using UnityEngine;

public class CallTeacherDetection : MonoBehaviour
{
    [SerializeField] private StudentCallLogic _studentCallLogic;

    [SerializeField] private CharacterAnimation _characterAnimation;

    private void Awake()
    {
        if (_studentCallLogic == null)
            throw new ArgumentNullException("_studentCallLogic");

        if (_characterAnimation == null)
            throw new ArgumentNullException("_characterAnimation");

        print("Initialized");
    }

    private void OnMouseDown()
    {
        if (_studentCallLogic.TeacherCalled)
            return;

        _characterAnimation.Animator.SetTrigger("Call");

        _studentCallLogic.CallTeacher(transform);
    }

}
