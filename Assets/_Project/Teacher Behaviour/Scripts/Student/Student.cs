using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Student : MonoBehaviour
{
    [Header("Local dependencies")]
    [SerializeField] private CharacterAnimation _characterAnimation;
    [SerializeField] private GameObject _targetHolder;
    [SerializeField] private GameObject _ballPrefab;

    [Header("Dependencies that require initialization")]
    [SerializeField] private NPC_ThrowController _npcThrowController;
    [SerializeField] private CallTeacherDetection _callTeacherDetection;
    [SerializeField] private StudentAI _studentAI;

    public StudentsManager StudentsManager { get; private set; }

    public NPC_ThrowController NPC_ThrowController => _npcThrowController;
    public Animator Animator => _characterAnimation.Animator;

    public bool HasFinished => _studentAI.HasFinished;

    public void Initialize(StudentsManager studentsManager, StudentCallLogic studentCallLogic, Transform player, Transform exitRoomTransform)
    {
        StudentsManager = studentsManager;

        _callTeacherDetection.Initialize(studentCallLogic);
        _npcThrowController.Initialize(player);
        _studentAI.Initialize(exitRoomTransform);
    }

    public void RemoveStudentFromManager()
    {
        StudentsManager.RemoveStudent(this);
    }

    private void Awake()
    {
        if (_npcThrowController == null)
            throw new ArgumentNullException("_npcThrowController");
        if (_characterAnimation == null)
            throw new ArgumentNullException("_characterAnimation");
        if (_targetHolder == null)
            throw new ArgumentNullException("_targetHolder");
        if (_ballPrefab == null)
            throw new ArgumentNullException("_ballPrefab");
        if (_callTeacherDetection == null)
            throw new ArgumentNullException("_callTeacherDetection");
        if (_studentAI == null)
            throw new ArgumentNullException("_studentAI");

        _targetHolder.SetActive(false);
    }

    public void GenerateBall()
    {
        BallController instance = Instantiate(_ballPrefab, NPC_ThrowController.GetThrowStartingPoint.position, Quaternion.identity).GetComponent<BallController>();
        instance.Initialize(this, (ExamElement)UnityEngine.Random.Range(1, 5));

        NPC_ThrowController.ThrowBall(instance.GetComponent<Rigidbody2D>());
    }

    public void HolderActive(bool active)
    {
        _targetHolder.SetActive(active);
    }
}
