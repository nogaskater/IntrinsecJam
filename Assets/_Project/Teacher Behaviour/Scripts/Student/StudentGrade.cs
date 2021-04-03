using System;
using UnityEngine;

public class StudentGrade : MonoBehaviour
{
    [SerializeField] private Student _student;
    [SerializeField] private StudentScoreFeedback _studentScoreFeedback;

    [Header("Settings")]
    [SerializeField] private int _minStartGrade = 2;
    [SerializeField] private int _maxStateGrade = 7;
    [SerializeField] private float _pointsMultiplier = 1.0f;

    public Action<bool> OnStudentFinished;
    public int Grade { get; private set; }

    public Action<int, int> OnScoreModified;

    private void Awake()
    {
        if (_student == null)
            throw new ArgumentNullException("_student");

        if (_studentScoreFeedback == null)
            throw new ArgumentNullException("_studentScoreFeedback");

        Grade = UnityEngine.Random.Range(_minStartGrade, _maxStateGrade);

        ModifyScore(0, false);
    }    

    public void CheckBallAnswers(BallController ballController)
    {
        if (Grade <= 0 || Grade >= 10)
            return;

        if (ballController.Answer == ballController.Answer)
            ModifyScore(2, true);
        else
            ModifyScore(-2, true);

        if (Grade > 0 || Grade < 10)
            AudioManager.Instance.PlaySound("Catch");

        Destroy(ballController.gameObject);
    }

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

            _student.RemoveStudentFromManager();

            Grade = 0;
        }
        else if(Grade >= 10)
        {

            pointsObtained += 1000;

            OnStudentFinished?.Invoke(true);

            _student.RemoveStudentFromManager();

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