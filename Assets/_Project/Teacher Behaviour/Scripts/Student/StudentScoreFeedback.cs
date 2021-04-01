using System;
using UnityEngine;
public class StudentScoreFeedback : MonoBehaviour
{
    [SerializeField] private ScorePopUp _popUpPrefab;
    [SerializeField] private ScorePopUp _popUpNegativePrefab;

    private void Awake()
    {
        if(_popUpPrefab == null)
            throw new ArgumentNullException("_popUpPrefab");
        if (_popUpNegativePrefab == null)
            throw new ArgumentNullException("_popUpNegativePrefab");
    }

    public void PopUpFeedback(string score)
    {
        ScorePopUp popUp = Instantiate(_popUpPrefab, transform.position, Quaternion.identity);
        popUp.Initialize(score);
    }
    public void PopUpNegativeFeedback(string score)
    {
        ScorePopUp popUp = Instantiate(_popUpNegativePrefab, transform.position, Quaternion.identity);
        popUp.Initialize(score);
    }
}
