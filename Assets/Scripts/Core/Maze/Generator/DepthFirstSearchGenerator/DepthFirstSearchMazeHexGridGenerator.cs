using System;
using System.Collections.Generic;

public class DepthFirstSearchMazeHexGridGenerator : DepthFirstSearchMazeGridGenerator
{
    protected override MazeDataGrid BuildInitialGrid(int width, int height) => new HexMazeDataGrid(width, height);

    protected override List<MazeCellData> GetUnvisitedNeighbours(MazeCellData currentCell, MazeDataGrid mazeGrid, Dictionary<uint, MazeCellData> visitedCells)
    {
        List<MazeCellData> unvisitedNeighbours = new List<MazeCellData>();

        AxialGridCoordinates axialCellCoordinates = currentCell.Coordinates.ConvertToGridAxial();

        foreach (KeyValuePair<CellDirections, AxialGridCoordinates> direction in HexGridDirections.Directions)
        {
            IGridCoordinates neighbourCoordinates = axialCellCoordinates + direction.Value;

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
            case CellDirections.UpLeft:
                currentCell.Walls[CellDirections.UpLeft] = false;
                chosenCell.Walls[CellDirections.DownRight] = false;
                break;
            case CellDirections.UpRight:
                currentCell.Walls[CellDirections.UpRight] = false;
                chosenCell.Walls[CellDirections.DownLeft] = false;
                break;
            case CellDirections.Right:
                currentCell.Walls[CellDirections.Right] = false;
                chosenCell.Walls[CellDirections.Left] = false;
                break;
            case CellDirections.DownRight:
                currentCell.Walls[CellDirections.DownRight] = false;
                chosenCell.Walls[CellDirections.UpLeft] = false;
                break;
            case CellDirections.DownLeft:
                currentCell.Walls[CellDirections.DownLeft] = false;
                chosenCell.Walls[CellDirections.UpRight] = false;
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
        AxialGridCoordinates axialCellCoordinates = currentCell.Coordinates.ConvertToGridAxial();

        foreach (KeyValuePair<CellDirections, AxialGridCoordinates> direction in HexGridDirections.Directions)
        {
            IGridCoordinates coordinates = axialCellCoordinates + direction.Value;

            if (CheckCoordinatesMatch(neighbourCell.Coordinates, coordinates))
                return direction.Key;
        }

        throw new Exception("There cells was no connection");
    }
}
