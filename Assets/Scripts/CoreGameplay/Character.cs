using System;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IMovable
{
    [SerializeField] private float _speed;

    private Vector3 _positionFrom;
    private Vector3 _positionTo;

    public event Action TargetReached;

    private float _progressToTarget;
    private const float MaxProgressToTarget = 1f;

    private bool _isStop;

    public void Initialize()
    {
        _isStop = true;
        _progressToTarget = 0f;
    }

    public void SpawnTo(Vector3 position)
    {
        transform.position = position;
    }

    private void Update()
    {
        if (_isStop)
            return;

        _progressToTarget += Time.deltaTime * _speed;
        transform.position = Vector3.LerpUnclamped(_positionFrom, _positionTo, _progressToTarget);

        if (_progressToTarget >= MaxProgressToTarget)
        {
            transform.position = _positionTo;
            _progressToTarget = 0;
            TargetReached?.Invoke();
        }
    }

    public void Move(Vector3 positionFrom, Vector3 positionTo)
    {
        _positionFrom = positionFrom;
        _positionTo = positionTo;

        Vector3 moveDirection = _positionTo - _positionFrom;
        transform.rotation = Quaternion.LookRotation(moveDirection);

        _progressToTarget = 0;

        _isStop = false;
    }

    public void Stop()
    {
        _isStop = true;
    }
}
