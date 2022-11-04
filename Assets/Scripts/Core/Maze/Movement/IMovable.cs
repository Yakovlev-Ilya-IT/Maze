using System;
using UnityEngine;

public interface IMovable
{
    public event Action TargetReached;

    public void Move(Vector3 positionFrom, Vector3 positionTo);
    public void Stop();
}
