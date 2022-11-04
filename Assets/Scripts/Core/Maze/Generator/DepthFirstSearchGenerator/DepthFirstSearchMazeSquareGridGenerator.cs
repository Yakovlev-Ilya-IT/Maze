using System;
using System.Collections.Generic;

public class DepthFirstSearchMazeSquareGridGenerator : DepthFirstSearchMazeGridGenerator
{
    protected override MazeDataGrid BuildInitialGrid(int width, int height) => new SquareMazeDataGrid(width, height);

    protected override List<MazeCellData> GetUnvisitedNeighbours(MazeCellData currentCell, MazeDataGrid mazeGrid, Dictionary<uint, MazeCellData> visitedCells)
    {
        List<MazeCellData> unvisitedNeighbours = new List<MazeCellData>();

        CartesianGridCoordinates cartesianCellCoordinates = currentCell.Coordinates.ConvertToGridCartesian();

        foreach (KeyValuePair<CellDirections, CartesianGridCoordinates> direction in SquareGridDirections.Directions)
        {
            IGridCoordinates neighbourCoordinates = cartesianCellCoordinates + direction.Value;

            if (TryGetNeighbour(neighbourCoordinates, mazeGrid, out MazeCellData neighbour))
            {
                if (!visitedCells.ContainsKey(neighbour.Id))
                    unvisitedNeighbours.Add(neighbour);
            }
        }

        return unvisitedNeighbours;
    }

    protected override void RemoveWall(MazeCellData currentCell, MazeCellData chosenCell, CellDirections connectionDirection)
    {
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
                throw new ArgumentException(message: $"Unexpected enum value: {connectionDirection}", paramName: nameof(connectionDirection));
        }
    }

    protected override CellDirections GetConnectionDirection(MazeCellData currentCell, MazeCellData neighbourCell)
    {
        CartesianGridCoordinates cartesianCellCoordinates = currentCell.Coordinates.ConvertToGridCartesian();

        foreach (KeyValuePair<CellDirections, CartesianGridCoordinates> direction in SquareGridDirections.Directions)
        {
            IGridCoordinates coordinates = cartesianCellCoordinates + direction.Value;

            if (CheckCoordinatesMatch(neighbourCell.Coordinates, coordinates))
                return direction.Key;
        }

        throw new Exception("There cells was no connection");
    }
}
