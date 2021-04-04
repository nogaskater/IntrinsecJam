using System;
using UnityEngine;

public class CallTeacherDetection : MonoBehaviour
{
    [SerializeField] private Student _student;
    [SerializeField] private Transform _cooldownPoint;

    private StudentCallLogic _studentCallLogic;


    public void Initialize(StudentCallLogic studentCallLogic)
    {
        _studentCallLogic = studentCallLogic;
    }

    private void Awake()
    {
        if (_student == null)
            throw new ArgumentNullException("_student");
        if (_cooldownPoint == null)
            throw new ArgumentNullException("_cooldownPoint");
    }

    private void OnMouseDown()
    {
        if (_studentCallLogic.TeacherCalled || _studentCallLogic.CurrentPaper != null)
            return;

        if (_student.HasFinished)
            return;

        _studentCallLogic.HideCooldown();

        _student.Animator.SetTrigger("Call");

        _studentCallLogic.CallTeacher(transform);
    }

    private void OnMouseExit()
    {
        _studentCallLogic.HideCooldown();
    }

    private void OnMouseOver()
    {
        if (_studentCallLogic.TeacherCalled || _studentCallLogic.CurrentPaper != null)
            return;

        if (_student.HasFinished)
            return;

        _studentCallLogic.ShowCooldown(_cooldownPoint);
    }

}
