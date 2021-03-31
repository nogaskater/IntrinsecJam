using System;
using UnityEngine;

public class StudentCallLogic : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private CheckStudent _checkStudentState;

    [Header("Settings")]
    [SerializeField] private float _teacherCallCooldown = 30.0f;

    private bool _teacherCalled = false;
    public bool TeacherCalled => _teacherCalled;

    private float _teacherCallCounter;

    public float TeacherCallCounter => _teacherCallCounter;


    private void Awake()
    {
        if (_checkStudentState == null)
            throw new ArgumentNullException("_teacherCallCooldown");

        _teacherCallCounter = _teacherCallCooldown;
    }


    private void Update()
    {
        if(_teacherCalled)
        {
            if(_teacherCallCounter < _teacherCallCooldown)
                _teacherCallCounter += Time.deltaTime;
            else
            {
                _teacherCalled = false;
            }
        }
    }

    public void CallTeacher(Transform studentTransform)
    {
        _teacherCallCounter = 0;

        _teacherCalled = true;

        _checkStudentState.CalledByStudent(studentTransform);
    }

    public float GetCurrentCooldownNormalized()
    {
        return _teacherCallCounter / _teacherCallCooldown;
    }
}
