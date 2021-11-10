using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpeedManager : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private float _startSpeed;
    [SerializeField] private float _speedIncrement;
    [SerializeField] private float _starSpeedMultiplier;
    [SerializeField] private float _speedLimit;
    [SerializeField] private float _currentSpeed;

    public event UnityAction SpeedChange;
    public float CurrentSpeed => _currentSpeed;
    public float SpeedLimit => _speedLimit;
    public float StartSpeed => _startSpeed;
    public float SpeedIncrement => _speedIncrement;

    private void Start()
    {
        OnSpeedChange();
    }

    private void OnEnable()
    {
        _spawner.LevelChange += OnSpeedChange;
        _player.PowerUp += OnSpeedChange;
        _player.PowerDown += OnSpeedChange;
    }

    private void OnDisable()
    {
        _spawner.LevelChange -= OnSpeedChange;
        _player.PowerUp -= OnSpeedChange;
        _player.PowerDown -= OnSpeedChange;
    }

    private void OnSpeedChange()
    {
        float tmpCurrentSpeed;

        if (_player.IsPowerUp)
        {
            tmpCurrentSpeed = (_startSpeed + _spawner.CurrentLevel * _speedIncrement) * _starSpeedMultiplier;
        }
        else
        {
            tmpCurrentSpeed = _startSpeed + _spawner.CurrentLevel * _speedIncrement;
        }

        if (tmpCurrentSpeed >= _speedLimit)
        {
            tmpCurrentSpeed = _speedLimit;
        }

        _currentSpeed = tmpCurrentSpeed;
        SpeedChange?.Invoke();
    }
}
