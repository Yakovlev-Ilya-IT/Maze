using UnityEngine;

[CreateAssetMenu(fileName = "MovementDisplayControlFactory", menuName = "Factories/DisplayControls")]
public class MovementDisplayControlFactory : MazeMovementControlFactory
{
    [SerializeField] private SquareMovementControlDisplay _squareControl;
    [SerializeField] private HexMovementControlDisplay _hexControl;

    public override IMazeMovementControl Get(MazeCellType type, Transform bindingTarget)
    {
        MovementControlDisplay instance = Instantiate(GetControl(type));
        instance.Initialize(bindingTarget);   
        return instance;
    }

    private MovementControlDisplay GetControl(MazeCellType type)
    {
        switch (type)
        {
            case MazeCellType.Square:
                return _squareControl;
            case MazeCellType.Hex:
                return _hexControl;
        }

        Debug.LogError($"No prefab for {type}");
        return _squareControl;
    }
}
