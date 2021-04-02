using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralBallManager : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private ScoreManager _scoreManager;


    [Header("GameObject References")]
    [SerializeField] private Transform _npcsParent;
    [SerializeField] private List<NPC_ThrowController> _npcs = new List<NPC_ThrowController>();
    [SerializeField] private List<Rigidbody2D> _balls = new List<Rigidbody2D>();
    [SerializeField] private GameObject _ballPrefab;

    [Header("Manager Settings")]
    //[SerializeField] private int _maxConcurrentBalls;
    [SerializeField] private float _minSpawnInterval;
    [SerializeField] private float _maxSpawnInterval;
    [Header("First interval")]
    [SerializeField] private float _minFirstSpawnInterval = 2.0f;
    [SerializeField] private float _maxFirstSpawnInterval = 4.0f;

    [SerializeField] private int numQuestions = 6;

    //--Controlling papersID's--//
    private int _currentBallID = 0;

    //--Controlling spawn times--//
    private float lastSpawnTime= 0.0f;
    private float intervalToSpawn;

    private void Awake()
    {
        if (_scoreManager == null)
            throw new ArgumentNullException("_scoreManager");

        if (_npcsParent == null)
            throw new ArgumentNullException("_npcsParent");

        for (int i = 0; i < _npcsParent.childCount; i++)
        {
            Transform student = _npcsParent.GetChild(i);

            if(student.gameObject.activeSelf)
                _npcs.Add(student.GetComponent<NPC_ThrowController>());
        }

        foreach (var student in _npcs)
        {
            _scoreManager.AddNewStudentScore(student.GetComponent<StudentScore>());
        }

        _scoreManager.OnTimeOut += CheckIfStudentHavePassed;
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
        int randomNPC = UnityEngine.Random.Range(0, _npcs.Count);

        BallController instance = Instantiate(_ballPrefab, _npcs[randomNPC].GetThrowStartingPoint().position, Quaternion.identity).GetComponent<BallController>();    
        instance.Initialize(_npcs[randomNPC].GetComponent<Student>(), (ExamElement)UnityEngine.Random.Range(1, numQuestions));

        lastSpawnTime = Time.time;

        _npcs[randomNPC].ThrowBall(instance.GetComponent<Rigidbody2D>());
    }

    private void CheckBallSpawnAvailability()
    {
        if (_npcs.Count == 0)
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
        _npcs.Remove(throwController);

        if(_npcs.Count == 0)
        {
            _scoreManager.GameFinished();
        }
    }

    public StudentScore GetClosestStudent(Transform fromTeacher)
    {
        if (_npcs.Count == 0)
            return null;

        Transform closest = _npcs[0].transform;

        foreach (var student in _npcs)
        {
            if(Vector2.Distance(student.transform.position, fromTeacher.position) < Vector2.Distance(closest.position, fromTeacher.position))
            {
                closest = student.transform;
            }
        }

        return closest.GetComponent<StudentScore>();

    }

    public void CheckIfStudentHavePassed()
    {
        foreach (var student in _npcs)
        {
            if (student.GetComponent<StudentScore>().Grade < 5)
                _scoreManager.ModifyLives(-1);

            student.GetComponent<StudentAI>().FinishStudent();
        }

        _npcs.Clear();
    }
}
