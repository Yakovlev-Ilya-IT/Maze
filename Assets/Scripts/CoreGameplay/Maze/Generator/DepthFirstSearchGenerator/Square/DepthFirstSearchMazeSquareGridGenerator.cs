using System;
using System.Collections.Generic;

public class DepthFirstSearchMazeSquareGridGenerator : DepthFirstSearchMazeGridGenerator
{
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
