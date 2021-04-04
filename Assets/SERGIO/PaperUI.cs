using System;
using UnityEngine;

public class PaperUI : MonoBehaviour
{
    [SerializeField] private GameObject _selector;

    private void Awake()
    {
        if (_selector == null)
            throw new ArgumentNullException("_selector");

        _selector.SetActive(false);
    }

    public void SelectorActive(bool active)
    {
        _selector.SetActive(active);
    }
}
