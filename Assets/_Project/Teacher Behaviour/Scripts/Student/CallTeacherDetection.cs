using System;
using UnityEngine;

public class CallTeacherDetection : MonoBehaviour
{
    [SerializeField] private Student _student;

    private StudentCallLogic _studentCallLogic;


    public void Initialize(StudentCallLogic studentCallLogic)
    {
        _studentCallLogic = studentCallLogic;
    }

    private void Awake()
    {
        if (_student == null)
            throw new ArgumentNullException("_student");
    }

    private void OnMouseDown()
    {
        if (_studentCallLogic.TeacherCalled || _studentCallLogic.IsTableOpened)
            return;

        if (_student.HasFinished)
            return;

        _student.Animator.SetTrigger("Call");

        _studentCallLogic.CallTeacher(transform);
    }

}
