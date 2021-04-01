using System;
using UnityEngine;

public class StudentScore : MonoBehaviour
{
    [SerializeField] private ScoreManager _scoreManager;

    [SerializeField] private NPCBallTransitionController _nPCBallTransitionController;
    [SerializeField] private NPC_ThrowController _npc_ThrowController;

    [SerializeField] private StudentScoreFeedback _studentScoreFeedback;

    [Header("Settings")]
    [SerializeField] private int _minStartScore = 2;
    [SerializeField] private int _maxStartScore = 7;

    public Action<bool> OnStudentFinished;

    private void Awake()
    {
        if (_scoreManager == null)
            throw new ArgumentNullException("_scoreManager");

        if (_nPCBallTransitionController == null)
            throw new ArgumentNullException("_nPCBallTransitionController");

        _nPCBallTransitionController.OnBallReceived += ModifyScore;


        if (_npc_ThrowController == null)
            throw new ArgumentNullException("_npc_ThrowController");

        if (_studentScoreFeedback == null)
            throw new ArgumentNullException("_studentScoreFeedback");

        _grade = UnityEngine.Random.Range(_minStartScore, _maxStartScore);

        ModifyScore(0, false);
    }

    private int _grade;
    public int Grade => _grade;

    private int _points = 0;
    public int Points => _points;
    [SerializeField] private float _pointsMultiplier = 1.0f;
    

    public Action<int, int> OnScoreModified;
    public void ModifyScore(int delta, bool popUp)
    {
        _grade += delta;

        int pointsObtained = 0;
        if(delta > 0)
            pointsObtained = (int)(delta * 100 * _pointsMultiplier);



        if(_grade <= 0)
        {
            _scoreManager.ModifyLives(-1);

            OnStudentFinished?.Invoke(false);

            _npc_ThrowController.RemoveStudentFromThrowManager();

            _grade = 0;
        }
        else if(_grade >= 10)
        {

            pointsObtained += 1000;

            OnStudentFinished?.Invoke(true);

            _npc_ThrowController.RemoveStudentFromThrowManager();

            _grade = 10;

        }

        _points += pointsObtained;

        if(popUp)
        {
            if (delta > 0)
                _studentScoreFeedback.PopUpFeedback("+" + delta);
            else
                _studentScoreFeedback.PopUpNegativeFeedback(delta.ToString());

        }

        OnScoreModified?.Invoke(_grade, pointsObtained);
    }
}