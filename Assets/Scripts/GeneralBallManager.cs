using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralBallManager : MonoBehaviour
{
    [Header("GameObject References")]
    [SerializeField] private List<NPC_ThrowController> _npcs = new List<NPC_ThrowController>();
    [SerializeField] private List<Rigidbody2D> _balls = new List<Rigidbody2D>();
    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] private ThrowController _player;

    [Header("Manager Settings")]
    //[SerializeField] private int _maxConcurrentBalls;
    [SerializeField] private float _minSpawnInterval;
    [SerializeField] private float _maxSpawnInterval;

    //--Controlling papersID's--//
    private int _currentBallID = 0;

    //--Controlling spawn times--//
    private float lastSpawnTime= 0.0f;
    private float intervalToSpawn;

    void Start()
    {
        intervalToSpawn = Random.Range(_minSpawnInterval, _maxSpawnInterval);
    }
    void Update()
    {
        CheckBallSpawnAvailability();
    }

    private void GenerateRandomBall()
    {
        int randomNPC = Random.Range(0, _npcs.Count);

        GameObject instance = Instantiate(_ballPrefab, _npcs[randomNPC].GetThrowStartingPoint().position, Quaternion.identity);    
        instance.GetComponent<BallController>()._ballPaper.student_ID = _npcs[randomNPC].GetComponent<Student>().id; 
        instance.GetComponent<BallController>()._ballPaper.answer = ExamElement.NONE; 

        lastSpawnTime = Time.time;

        _npcs[randomNPC].ThrowBall(instance.GetComponent<Rigidbody2D>());

    }

    private void CheckBallSpawnAvailability()
    {
        if(Time.time > lastSpawnTime + intervalToSpawn /*&& currentConcurrentBalls < _maxConcurrentBalls*/)
        {
            GenerateRandomBall();
            intervalToSpawn = Random.Range(_minSpawnInterval, _maxSpawnInterval);
        }
    }

    public void RemoveCurrentBallFromController(Rigidbody2D rb)
    {
        foreach(Rigidbody2D b in _balls)
        {
            if(b == rb)
            {
                _balls.Remove(b);

                /*if (_player.GetActiveBall() == b)
                    _player.SetActiveBall(null);*/

                GameObject.Destroy(b.gameObject);
            }
        }
    }


}
