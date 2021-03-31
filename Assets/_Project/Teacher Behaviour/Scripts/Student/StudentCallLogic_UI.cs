using System;
using UnityEngine;
using UnityEngine.UI;

public class StudentCallLogic_UI : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private StudentCallLogic _studentCallLogic;

    [Header("UI Elements")]
    [SerializeField] private Image _cooldownImage;

    private void Awake()
    {
        if (_studentCallLogic == null)
            throw new ArgumentNullException("_studentCallLogic");

        if (_cooldownImage == null)
            throw new ArgumentNullException("_cooldownImage");
    }

    private void Start()
    {
        _cooldownImage.fillAmount = _studentCallLogic.GetCurrentCooldownNormalized();
    }

    private void Update()
    {
        if(_studentCallLogic.TeacherCalled)
        {
            _cooldownImage.fillAmount = _studentCallLogic.GetCurrentCooldownNormalized();
        }
    }
}
