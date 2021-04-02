using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentsManager : MonoBehaviour
{
    [Header("GameObject References")]
    [SerializeField] private Transform _npcsParent;
    [SerializeField] private GameObject _ballPrefab;

    [Header("Manager Settings")]
    [SerializeField] private float _minSpawnInterval;
    [SerializeField] private float _maxSpawnInterval;
    [Header("First interval")]
    [SerializeField] private float _minFirstSpawnInterval = 2.0f;
    [SerializeField] private float _maxFirstSpawnInterval = 4.0f;

    [SerializeField] private int numQuestions = 6;

    private List<NPC_ThrowController> _students = new List<NPC_ThrowController>();
    private List<Rigidbody2D> _balls = new List<Rigidbody2D>();

    //--Controlling papersID's--//
    private int _currentBallID = 0;

    //--Controlling spawn times--//
    private float lastSpawnTime= 0.0f;
    private float intervalToSpawn;

    private void Awake()
    {
        if (_npcsParent == null)
            throw new ArgumentNullException("_npcsParent");

        for (int i = 0; i < _npcsParent.childCount; i++)
        {
            NPC_ThrowController student = _npcsParent.GetChild(i).GetComponent<NPC_ThrowController>();

            student.Initialize(this);

            if(student.gameObject.activeSelf)
                _students.Add(student);
        }


    }

    private void OnEnable()
    {
        MatchManager.Instance.OnTimeOut += CheckIfStudentHavePassed;
    }
    private void OnDisable()
    {
        MatchManager.Instance.OnTimeOut -= CheckIfStudentHavePassed;
    }

    void Start()
    {
        intervalToSpawn = UnityEngine.Random.Range(_minFirstSpawnInterval, _maxFirstSpawnInterval);
    }
    void Update()
    {
        CheckBallSpawnAvailability();
    }

    private void GenerateRandomBall()
    {
        int randomNPC = UnityEngine.Random.Range(0, _students.Count);

        BallController instance = Instantiate(_ballPrefab, _students[randomNPC].GetThrowStartingPoint().position, Quaternion.identity).GetComponent<BallController>();    
        instance.Initialize(_students[randomNPC].GetComponent<Student>(), (ExamElement)UnityEngine.Random.Range(1, numQuestions));

        lastSpawnTime = Time.time;

        _students[randomNPC].ThrowBall(instance.GetComponent<Rigidbody2D>());
    }

    private void CheckBallSpawnAvailability()
    {
        if (_students.Count == 0)
            return;

        if(Time.time > lastSpawnTime + intervalToSpawn /*&& currentConcurrentBalls < _maxConcurrentBalls*/)
        {
            GenerateRandomBall();
            intervalToSpawn = UnityEngine.Random.Range(_minSpawnInterval, _maxSpawnInterval);
        }
    }

    public void RemoveCurrentBallFromController(Rigidbody2D rb)
    {
        foreach(Rigidbody2D b in _balls)
        {
            if(b == rb)
            {
                _balls.Remove(b);


                Destroy(b.gameObject);
            }
        }
    }

    public void RemoveStudent(NPC_ThrowController throwController)
    {
        _students.Remove(throwController);

        if(_students.Count == 0)
        {
            MatchManager.Instance.GameFinished();
        }
    }

    public StudentGrade GetClosestStudent(Transform fromTeacher)
    {
        if (_students.Count == 0)
            return null;

        Transform closest = _students[0].transform;

        foreach (var student in _students)
        {
            if(Vector2.Distance(student.transform.position, fromTeacher.position) < Vector2.Distance(closest.position, fromTeacher.position))
            {
                closest = student.transform;
            }
        }

        return closest.GetComponent<StudentGrade>();

    }

    public void CheckIfStudentHavePassed()
    {
        foreach (var student in _students)
        {
            if (student.GetComponent<StudentGrade>().Grade < 5)
                MatchManager.Instance.ModifyLives(-1);

            student.GetComponent<StudentAI>().FinishStudent();
        }

        _students.Clear();
    }
}
