using System;
using UnityEngine;

public class StudentScore : MonoBehaviour
{
    [SerializeField] private NPCBallTransitionController _nPCBallTransitionController;
    [SerializeField] private NPC_ThrowController _npc_ThrowController;

    [Header("Settings")]
    [SerializeField] private int _minStartScore = 2;
    [SerializeField] private int _maxStartScore = 7;

    public Action<bool> OnStudentFinished;

    private void Awake()
    {
        if (_nPCBallTransitionController == null)
            throw new ArgumentNullException("_nPCBallTransitionController");

        _nPCBallTransitionController.OnBallReceived += ModifyScore;


        if (_npc_ThrowController == null)
            throw new ArgumentNullException("_npc_ThrowController");

        _grade = UnityEngine.Random.Range(_minStartScore, _maxStartScore);

        ModifyScore(0);
    }

    private int _grade;
    public int Grade => _grade;

    private int _points = 0;
    public int Points => _points;
    [SerializeField] private float _pointsMultiplier = 1.0f;
    

    public Action<int, int> OnScoreModified;
    public void ModifyScore(int delta)
    {
        _grade += delta;

        int pointsObtained = (int)(delta * 100 * _pointsMultiplier);

        _points += pointsObtained;

        OnScoreModified?.Invoke(_grade, pointsObtained);

        if(_grade <= 0)
        {
            // HANDLE HEALTH MODIFICATION

            OnStudentFinished?.Invoke(false);

            _npc_ThrowController.RemoveStudentFromThrowManager();            
        }
        else if(_grade >= 10)
        {
            OnStudentFinished?.Invoke(true);

            _npc_ThrowController.RemoveStudentFromThrowManager();

        }
    }
}
