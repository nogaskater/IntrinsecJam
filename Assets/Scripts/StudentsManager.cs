using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentsManager : MonoBehaviour
{
    [Header("GameObject References")]
    [SerializeField] private StudentCallLogic _studentCallLogic;
    [SerializeField] private Transform _npcsParent;
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _exitRoomTransform;

    [Header("Manager Settings")]
    [SerializeField] private float _minSpawnInterval;
    [SerializeField] private float _maxSpawnInterval;
    [Header("First interval")]
    [SerializeField] private float _minFirstSpawnInterval = 2.0f;
    [SerializeField] private float _maxFirstSpawnInterval = 4.0f;

    private List<Student> _students = new List<Student>();


    //--Controlling spawn times--//
    private float _lastSpawnTime= 0.0f;
    private float _intervalToSpawn;

    private void Awake()
    {
        if (_studentCallLogic == null)
            throw new ArgumentNullException("_studentCallLogic");
        if (_npcsParent == null)
            throw new ArgumentNullException("_npcsParent");
        if (_player == null)
            throw new ArgumentNullException("_player");
        if (_exitRoomTransform == null)
            throw new ArgumentNullException("_exitRoomTransform");

        for (int i = 0; i < _npcsParent.childCount; i++)
        {
            Student student = _npcsParent.GetChild(i).GetComponent<Student>();

            student.Initialize(this, _studentCallLogic, _player, _exitRoomTransform);

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


    private void Start()
    {
        _intervalToSpawn = UnityEngine.Random.Range(_minFirstSpawnInterval, _maxFirstSpawnInterval);

        _lastSpawnTime = Time.time;

    }
    void Update()
    {
        CheckBallSpawnAvailability();
    }

    private void GenerateRandomBall()
    {
        int randomNPC = UnityEngine.Random.Range(0, _students.Count);

        _students[randomNPC].GenerateBall();

        _lastSpawnTime = Time.time;
    }

    private void CheckBallSpawnAvailability()
    {
        if (_students.Count == 0)
            return;

        if(Time.time > _lastSpawnTime + _intervalToSpawn /*&& currentConcurrentBalls < _maxConcurrentBalls*/)
        {
            GenerateRandomBall();
            _intervalToSpawn = UnityEngine.Random.Range(_minSpawnInterval, _maxSpawnInterval);
        }
    }


    public void RemoveStudent(Student throwController)
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
