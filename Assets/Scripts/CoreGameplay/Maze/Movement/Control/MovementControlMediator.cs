using System;
using System.Collections.Generic;
using UnityEngine;

public class MovementControlMediator
{
    private MovementHandler _movementHandler;

    private MazeMovementControlFactory _ñontrolFactory;
    private MovementControlType _controlType;
    private Transform _controlBinding;
    private IRotatable _controlRotationTarget;
    private MazeCellType _mazeCellType;
    private IMazeMovementControl _control;

    private const string ControlFactoryPath = "Controls";
    private readonly Dictionary<MazeCellType, string> _cellTypeToControlFactoryName = new Dictionary<MazeCellType, string>()
    {
        { MazeCellType.Square, "SquareMovementControlFactory"},
        { MazeCellType.Hex, "HexMovementControlFactory" }
    };

    private bool _isInit;
    private const string NotInitExceptionName = "MovementControlMediator not inited";

    public void Initialize(MovementHandler movementHandler, MovementControlType controlType, MazeCellType mazeCellType, Transform controlBinding, IRotatable controlRotationTarget)
    {
        _isInit = true;

        _movementHandler = movementHandler;

        _controlType = controlType;
        _mazeCellType = mazeCellType;
        _controlBinding = controlBinding;
        _controlRotationTarget = controlRotationTarget;

        SwitchControl();
    }

    public void SwitchControlBinding(Transform controlBinding, IRotatable controlRotationTarget)
    {
        if (!_isInit)
            throw new InvalidOperationException(NotInitExceptionName);

        _controlBinding = controlBinding;
        _controlRotationTarget = controlRotationTarget;

        SwitchControl();
    }

    public void SwitchControlType(MovementControlType controlType)
    {
        if (!_isInit)
            throw new InvalidOperationException(NotInitExceptionName);

        _controlType = controlType;
        SwitchControl();
    }

    public void SwitchCellType(MazeCellType mazeCellType)
    {
        if (!_isInit)
            throw new InvalidOperationException(NotInitExceptionName);

        _mazeCellType = mazeCellType;
        SwitchControl();
    }

    private void SwitchControl()
    {
        if (_control != null)
        {
            _control.DirectionSelected -= OnDirectionSelected;
            _control.Remove();
        }

        _ñontrolFactory = Resources.Load<MazeMovementControlFactory>($"{ControlFactoryPath}/{_cellTypeToControlFactoryName[_mazeCellType]}");

        _control = _ñontrolFactory.Get(_controlType, _controlBinding, _controlRotationTarget);
        _control.DirectionSelected += OnDirectionSelected;
        HideControl();
    }

    public void HideControl()
    {
        if (!_isInit)
            throw new InvalidOperationException(NotInitExceptionName);

        _control.Hide();
    }

    public void ShowControl(Dictionary<CellDirections, IGridCoordinates> directionToNeighboursCoordinates)
    {
        if (!_isInit)
            throw new InvalidOperationException(NotInitExceptionName);

        _control.Show(directionToNeighboursCoordinates);
    }

    private void OnDirectionSelected(CellDirections direction)
    {
        _movementHandler.StartMove(direction);
    }
}
