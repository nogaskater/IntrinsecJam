using System;
using UnityEngine;

public class StudentGrade : MonoBehaviour
{
    [SerializeField] private NPCBallTransitionController _nPCBallTransitionController;
    [SerializeField] private NPC_ThrowController _npc_ThrowController;

    [SerializeField] private StudentScoreFeedback _studentScoreFeedback;

    [Header("Settings")]
    [SerializeField] private int _minStartGrade = 2;
    [SerializeField] private int _maxStateGrade = 7;
    [SerializeField] private float _pointsMultiplier = 1.0f;

    public Action<bool> OnStudentFinished;
    public int Grade { get; private set; }

    private void Awake()
    {
        if (_nPCBallTransitionController == null)
            throw new ArgumentNullException("_nPCBallTransitionController");

        _nPCBallTransitionController.OnBallReceived += ModifyScore;


        if (_npc_ThrowController == null)
            throw new ArgumentNullException("_npc_ThrowController");

        if (_studentScoreFeedback == null)
            throw new ArgumentNullException("_studentScoreFeedback");

        Grade = UnityEngine.Random.Range(_minStartGrade, _maxStateGrade);

        ModifyScore(0, false);
    }    

    public Action<int, int> OnScoreModified;
    public void ModifyScore(int delta, bool popUp)
    {
        Grade += delta;

        int pointsObtained = 0;
        if(delta > 0)
            pointsObtained = (int)(delta * 100 * _pointsMultiplier);



        if(Grade <= 0)
        {
            MatchManager.Instance.ModifyLives(-1);

            OnStudentFinished?.Invoke(false);

            _npc_ThrowController.RemoveStudentFromThrowManager();

            Grade = 0;
        }
        else if(Grade >= 10)
        {

            pointsObtained += 1000;

            OnStudentFinished?.Invoke(true);

            _npc_ThrowController.RemoveStudentFromThrowManager();

            Grade = 10;

        }

        MatchManager.Instance.AddScore(pointsObtained);

        if(popUp)
        {
            if (delta > 0)
                _studentScoreFeedback.PopUpFeedback("+" + delta);
            else
                _studentScoreFeedback.PopUpNegativeFeedback(delta.ToString());

        }

        OnScoreModified?.Invoke(Grade, pointsObtained);
    }
}