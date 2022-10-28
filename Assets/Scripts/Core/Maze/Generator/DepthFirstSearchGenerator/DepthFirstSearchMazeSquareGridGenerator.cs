using System.Collections.Generic;
using UnityEngine;

public class DepthFirstSearchMazeSquareGridGenerator : DepthFirstSearchMazeGridGenerator
{
    protected override Grid BuildInitialGrid(int width, int height) => new SquareGrid(width, height);

    protected override List<CellData> GetUnvisitedNeighbours(CellData currentCell, Grid mazeGrid)
    {
        List<CellData> unvisitedNeighbours = new List<CellData>();

        CartesianGridCoordinates cartesianCellCoordinates = currentCell.Coordinates.ConvertToGridCartesian();

        foreach (KeyValuePair<CellDirections, Vector2Int> direction in SquareGridDirections.Directions)
        {
            IGridCoordinates neighbourCoordinates = new CartesianGridCoordinates(cartesianCellCoordinates.X + direction.Value.x, cartesianCellCoordinates.Y + direction.Value.y);

            if (TryGetNeighbour(neighbourCoordinates, mazeGrid, out CellData neighbour))
            {
                if (!_visitedCells.ContainsKey(neighbour.Id))
                    unvisitedNeighbours.Add(neighbour);
            }
        }

        return unvisitedNeighbours;
    }

    protected override void RemoveWall(CellData currentCell, CellData chosenCell)
    {
        CellDirections connectionDirection = GetConnectionDirection(currentCell, chosenCell);

        switch (connectionDirection)
        {
            case CellDirections.Up:
                currentCell.Walls[CellDirections.Up] = false;
                chosenCell.Walls[CellDirections.Down] = false;
                break;
            case CellDirections.Right:
                currentCell.Walls[CellDirections.Right] = false;
                chosenCell.Walls[CellDirections.Left] = false;
                break;
            case CellDirections.Down:
                currentCell.Walls[CellDirections.Down] = false;
                chosenCell.Walls[CellDirections.Up] = false;
                break;
            case CellDirections.Left:
                currentCell.Walls[CellDirections.Left] = false;
                chosenCell.Walls[CellDirections.Right] = false;
                break;
            default:
                Debug.LogError("there was no cartesian direction");
                break;
        }
    }

    private CellDirections GetConnectionDirection(CellData currentCell, CellData neighbourCell)
    {
        CartesianGridCoordinates cartesianCellCoordinates = currentCell.Coordinates.ConvertToGridCartesian();

        foreach (KeyValuePair<CellDirections, Vector2Int> direction in SquareGridDirections.Directions)
        {
            IGridCoordinates coordinates = new CartesianGridCoordinates(cartesianCellCoordinates.X + direction.Value.x, cartesianCellCoordinates.Y + direction.Value.y);

            if (CheckCoordinatesMatch(neighbourCell.Coordinates, coordinates))
                return direction.Key;
        }

        Debug.LogError("there was no connection");

        return CellDirections.Up;
    }
}
