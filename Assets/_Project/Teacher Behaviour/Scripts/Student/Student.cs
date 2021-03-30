using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Student : MonoBehaviour
{
    public int id;

    [SerializeField] private CheckStudent _checkStudent;

    private void Awake()
    {
        if (_checkStudent == null)
            throw new ArgumentNullException("_checkStudent");
    }

    private void OnMouseDown()
    {
        print("Has been clicked!");

        _checkStudent.CalledByStudent(transform);
    }
}
