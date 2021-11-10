using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : ObjectPool
{
    [SerializeField] private Player _player;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _pickUpHeartSpawnRate;
    [SerializeField] private float _starSpawnRate;
    [SerializeField] private float _firstStarSpawnDelay;
    [SerializeField] private float _firstHeartSpawnDelay;
    [SerializeField] private float _levelDuration;
    [SerializeField] private float _distanceBetweenObjects;
    [SerializeField] private int _currentLevel;

    private float _elapsedTime = 0;         // Âנולÿ ןנמרוהרוו ס םאקאכא
    private float _lastHeartSpawnTime;
    private float _lastStarSpawnTime;
    private float _currentSpawnRate;
    private int [] _previousPoints = new int[4];

    public event UnityAction LevelChange;

    public int CurrentLevel => _currentLevel;

    private void OnEnable()
    {
        speedManager.SpeedChange += OnSpeedChange;
    }

    private void OnDisable()
    {
        speedManager.SpeedChange -= OnSpeedChange;
    }

    private void Start()
    {
        _lastStarSpawnTime = _firstStarSpawnDelay;
        _lastHeartSpawnTime = _firstHeartSpawnDelay;
        _currentLevel = 1;
        InitPools();
    }

    private void FixedUpdate()
    {
        GetCurrentLevel();

        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= _currentSpawnRate)
        {
            if (_player.Health < _player.MaxHealth && Time.timeSinceLevelLoad >= (_lastHeartSpawnTime + _pickUpHeartSpawnRate))
            {
                TryCreateObjectFromPool(pickUpHeartPool);
                _lastHeartSpawnTime = Time.timeSinceLevelLoad;
            }
            else if (Time.timeSinceLevelLoad >= _lastStarSpawnTime + _starSpawnRate)
            {
                TryCreateObjectFromPool(starPool);
                _lastStarSpawnTime = Time.timeSinceLevelLoad;
            }
            else
            {
                int randomNumber = Random.Range(0, 100);

                if (_currentLevel < 4)
                {
                    if (randomNumber < 75)
                    {
                        TryCreateObjectFromPool(coinPool);
                    }
                    else
                    {
                        TryCreateObjectFromPool(enemyOnePool);
                    }
                }

                if (_currentLevel >= 4 && _currentLevel < 8)
                {
                    if (randomNumber < 63)
                    {
                        TryCreateObjectFromPool(coinPool);
                    }
                    else if (randomNumber >= 63 && randomNumber < 88)
                    {
                        TryCreateObjectFromPool(enemyOnePool);
                    }
                    else
                    {
                        TryCreateObjectFromPool(enemyTwoPool);
                    }
                }

                if (_currentLevel >= 8)
                {
                    if (randomNumber < 50)
                    {
                        TryCreateObjectFromPool(coinPool);
                    }
                    else if (randomNumber >= 50 && randomNumber < 75)
                    {
                        TryCreateObjectFromPool(enemyOnePool);
                    }
                    else
                    {
                        TryCreateObjectFromPool(enemyTwoPool);
                    }
                }
            }
        }
    }

    private void GetCurrentLevel()
    {
        if(Time.timeSinceLevelLoad >= _currentLevel * _levelDuration)
        {
            _currentLevel++;
            LevelChange?.Invoke();
        }
    }

    private void TryCreateObjectFromPool(List<GameObject> objectPool)
    {
        if (TryGetObject(objectPool, out GameObject objectFromPool))
        {
            _elapsedTime = 0;

            int spawnPointNumber = GenerateSpawnPointNumber();
            SetObject(objectFromPool, _spawnPoints[spawnPointNumber].position);
        }
    }

    private int GenerateSpawnPointNumber()
    {
        bool isAllPointsEqual = true;
        int amountOfPoints = _spawnPoints.Length;
        int spawnPointNumber = Random.Range(0, amountOfPoints);

        for (int i = amountOfPoints - 1; i > 0; i--)
        {
            _previousPoints[i] = _previousPoints[i - 1];
        }

        _previousPoints[0] = spawnPointNumber;

        for (int i = 0; i < amountOfPoints; i++)
        {
            if (_previousPoints[i] != _previousPoints[0])
            {
                isAllPointsEqual = false;
            }
        }

        if (isAllPointsEqual)
        {
            while (spawnPointNumber == _previousPoints[0])
            {
                spawnPointNumber = Random.Range(0, amountOfPoints);
            }
            _previousPoints[0] = spawnPointNumber;
        }

        return spawnPointNumber;
    }

    private void SetObject(GameObject spawnObject, Vector3 spawnPoint)
    {
        spawnObject.SetActive(true);
        spawnObject.transform.position = spawnPoint;
    }

    private void OnSpeedChange()
    {
        _currentSpawnRate = _distanceBetweenObjects / speedManager.CurrentSpeed;

        Debug.Log("Level: " + _currentLevel + 
                  "   Speed: " + speedManager.CurrentSpeed + 
                  "   SpawnRate: " + _currentSpawnRate +
                  "   isPowerUp: " + _player.IsPowerUp);
    }
}
