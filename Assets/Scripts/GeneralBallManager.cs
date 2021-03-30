using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralBallManager : MonoBehaviour
{
    [Header("GameObject References")]
    [SerializeField] private List<GameObject> _npcs = new List<GameObject>();
    [SerializeField] private List<GameObject> _balls = new List<GameObject>();
    [SerializeField] private GameObject _ballPrefab;

    [Header("Manager Settings")]
    [SerializeField] private int _maxConcurrentBalls;
    [SerializeField] private float _minSpawnInterval;
    [SerializeField] private float _maxSpawnInterval;

    //--Controlling amount of NPC's ball--//
    private int currentConcurrentBalls = 0;

    //--Controlling papersID's--//
    private int _currentBallID = 0;

    //--Controlling spawn times--//
    private float lastSpawnTime= 0.0f;

    void Start()
    {
        
    }
    void Update()
    {
        
    }

    private void GenerateRandomBall()
    {
        int randomNPC = Random.Range(0, _npcs.Count);

        GameObject instance = Instantiate(_ballPrefab, _npcs[randomNPC].GetComponent<NPC_ThrowController>().GetThrowStartingPoint().position, Quaternion.identity);     //Not ideal using the get component...

        currentConcurrentBalls++;

    }

    private void CheckBallSpawnAvailability()
    {
        float intervalToSpawn = Random.Range(_minSpawnInterval, _maxSpawnInterval);

        if(Time.time <= lastSpawnTime + intervalToSpawn && currentConcurrentBalls < _maxConcurrentBalls)
        {
            GenerateRandomBall();
        }
    }

    public void RemoveCurrentBallFromController(int pID)
    {
        foreach(GameObject b in _balls)
        {
            if(b.GetComponent<BallController>().GetPaperID() == pID)
            {
                //Check if the answer is correct or not
                //--TO-DO--//
                _balls.Remove(b);
                GameObject.Destroy(b);
                currentConcurrentBalls--;
            }
        }
    }


}
