using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SquareMovementControlFactory", menuName = "Factories/ControlFactories/SquareControlFactory")]
public class SquareMazeMovementControlFactory : MazeMovementControlFactory
{
    [SerializeField] private SquareMovementControlDisplay _worldSpaceDisplay;

    public override IMazeMovementControl Get(MovementControlType type, Transform bindingTarget)
    {
        switch (type)
        {
            case MovementControlType.WorldSpaceDisplay:
                SquareMovementControlDisplay instance = Instantiate(_worldSpaceDisplay);
                instance.Initialize(bindingTarget);
                return instance;
        }

        throw new ArgumentException($"No prefab for {type}");
    }
}
