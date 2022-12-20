using System;
using UnityEngine;

[CreateAssetMenu(fileName = "HexMovementControlFactory", menuName = "Factories/ControlFactories/HexControlFactory")]
public class HexMazeMovementControlFactory : MazeMovementControlFactory
{
    [SerializeField] private HexMovementControlDisplay _worldSpaceDisplay;

    public override IMazeMovementControl Get(MovementControlType type, Transform bindingTarget, IRotatable rotationTarget)
    {
        switch (type)
        {
            case MovementControlType.WorldSpaceDisplay:
                MovementControlDisplay instance = Instantiate(_worldSpaceDisplay);
                instance.Initialize(bindingTarget, rotationTarget);
                return instance;
        }

        throw new ArgumentException($"No prefab for {type}");
    }
}
