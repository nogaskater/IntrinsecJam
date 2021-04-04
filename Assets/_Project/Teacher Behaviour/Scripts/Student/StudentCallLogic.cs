using System;
using UnityEngine;


public class StudentCallLogic : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CheckStudent _checkStudentState;
    [SerializeField] private TableBehaviour _tableBehaviour;

    [SerializeField] private Transform _studentCooldownUI;

    [Header("Settings")]
    [SerializeField] private float _teacherCallCooldown = 30.0f;


    private bool _teacherCalled = false;
    public bool TeacherCalled => _teacherCalled;
    private float _teacherCallCounter;

    public float TeacherCallCounter => _teacherCallCounter;

    public PaperUI CurrentPaper => _tableBehaviour.CurrentPaperUIAtTable;
    private void Awake()
    {
        if (_checkStudentState == null)
            throw new ArgumentNullException("_teacherCallCooldown");
        if (_tableBehaviour == null)
            throw new ArgumentNullException("_tableBehaviour");
        if (_studentCooldownUI == null)
            throw new ArgumentNullException("_studentCooldownUI");

        _teacherCallCounter = _teacherCallCooldown;

        HideCooldown();
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

    public void ShowCooldown(Transform transform)
    {
        _studentCooldownUI.position = transform.position;

        _studentCooldownUI.gameObject.SetActive(true);
    }

    public void HideCooldown()
    {
        _studentCooldownUI.gameObject.SetActive(false);
    }
}
