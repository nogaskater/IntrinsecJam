using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Student : MonoBehaviour
{
    [SerializeField] private GameObject _targetHolder;

    private void Awake()
    {
        if (_targetHolder == null)
            throw new ArgumentNullException("_targetHolder");

        _targetHolder.SetActive(false);
    }

    public void HolderActive(bool active)
    {
        _targetHolder.SetActive(active);
    }
}
