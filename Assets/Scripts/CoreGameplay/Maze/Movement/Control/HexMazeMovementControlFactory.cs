using System;
using UnityEngine;

[CreateAssetMenu(fileName = "HexMovementControlFactory", menuName = "Factories/ControlFactories/HexControlFactory")]
public class HexMazeMovementControlFactory : MazeMovementControlFactory
{
    [SerializeField] private HexMovementControlDisplay _worldSpaceDisplay;

    public override IMazeMovementControl Get(MovementControlType type, Transform bindingTarget)
    {
        switch (type)
        {
            case MovementControlType.WorldSpaceDisplay:
                MovementControlDisplay instance = Instantiate(_worldSpaceDisplay);
                instance.Initialize(bindingTarget);
                return instance;
        }

        throw new ArgumentException($"No prefab for {type}");
    }
}
