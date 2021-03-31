using System;
using UnityEngine;

public class CallTeacherDetection : MonoBehaviour
{
    [SerializeField] private StudentCallLogic _studentCallLogic;

    private void Awake()
    {
        if (_studentCallLogic == null)
            throw new ArgumentNullException("_studentCallLogic");

        print("Initialized");
    }

    private void OnMouseDown()
    {
        if (_studentCallLogic.TeacherCalled)
            return;

        _studentCallLogic.CallTeacher(transform);
    }

}
