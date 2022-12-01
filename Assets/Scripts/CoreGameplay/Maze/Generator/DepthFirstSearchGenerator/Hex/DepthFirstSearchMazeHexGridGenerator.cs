using System;
using System.Collections.Generic;

public class DepthFirstSearchMazeHexGridGenerator : DepthFirstSearchMazeGridGenerator
{
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
