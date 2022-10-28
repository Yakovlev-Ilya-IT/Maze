using System.Collections.Generic;
using UnityEngine;

public class DepthFirstSearchMazeHexGridGenerator : DepthFirstSearchMazeGridGenerator
{
    protected override Grid BuildInitialGrid(int width, int height) => new HexGrid(width, height);

    protected override List<CellData> GetUnvisitedNeighbours(CellData currentCell, Grid mazeGrid)
    {
        List<CellData> unvisitedNeighbours = new List<CellData>();

        AxialGridCoordinates axialCellCoordinates = currentCell.Coordinates.ConvertToGridAxial();

        foreach (KeyValuePair<CellDirections, Vector2Int> direction in HexGridDirections.Directions)
        {
            IGridCoordinates neighbourCoordinates = new AxialGridCoordinates(axialCellCoordinates.X + direction.Value.x, axialCellCoordinates.Y + direction.Value.y);

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
                Debug.LogError("there was no hex direction");
                break;
        }
    }

    private CellDirections GetConnectionDirection(CellData currentCell, CellData neighbourCell)
    {
        AxialGridCoordinates axialCellCoordinates = currentCell.Coordinates.ConvertToGridAxial();

        foreach (KeyValuePair<CellDirections, Vector2Int> direction in HexGridDirections.Directions)
        {
            IGridCoordinates coordinates = new AxialGridCoordinates(axialCellCoordinates.X + direction.Value.x, axialCellCoordinates.Y + direction.Value.y);

            if (CheckCoordinatesMatch(neighbourCell.Coordinates, coordinates))
                return direction.Key;
        }

        Debug.LogError("there was no connection");

        return CellDirections.UpLeft;
    }
}
