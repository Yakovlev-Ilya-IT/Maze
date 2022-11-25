using System.Collections.Generic;
using UnityEngine;

public abstract class DepthFirstSearchMazeGridGenerator : IMazeGridGenerator
{
    private Dictionary<uint, MazeCellData> _visitedCells = new Dictionary<uint, MazeCellData>();

    public MazeDataGrid Generate(IMazeGridForm form)
    {
        MazeDataGrid mazeGrid = BuildInitialGrid(form);

        GenerateMaze(mazeGrid);

        return mazeGrid;

    }

    private MazeDataGrid BuildInitialGrid(IMazeGridForm form) => new MazeDataGrid(form);

    private void GenerateMaze(MazeDataGrid mazeGrid)
    {
        MazeCellData currentCell = mazeGrid.GetRandomCell();

        _visitedCells.Add(currentCell.Id, currentCell);

        Stack<MazeCellData> mazeCells = new Stack<MazeCellData>();

        mazeCells.Push(currentCell);

        while (mazeCells.Count > 0)
        {
            currentCell = mazeCells.Pop();

            List<MazeCellData> unvisitedNeighbours = GetUnvisitedNeighbours(currentCell, mazeGrid, _visitedCells);

            if (unvisitedNeighbours.Count > 0)
            {
                mazeCells.Push(currentCell);

                MazeCellData chosenCell = unvisitedNeighbours[Random.Range(0, unvisitedNeighbours.Count)];

                CellDirections conectionFromCurrentToChosen = GetConnectionDirection(currentCell, chosenCell);
                CellDirections conectionFromChosenToCurrent= GetConnectionDirection(chosenCell, currentCell);

                currentCell.AddNeighbourCoordinates(conectionFromCurrentToChosen, chosenCell.Coordinates);
                chosenCell.AddNeighbourCoordinates(conectionFromChosenToCurrent, currentCell.Coordinates);

                //RemoveWall(currentCell, chosenCell, connectionDirection);

                _visitedCells.Add(chosenCell.Id, chosenCell);
                mazeCells.Push(chosenCell);
            }
        }
    }

    protected abstract CellDirections GetConnectionDirection(MazeCellData currentCell, MazeCellData neighbourCell);

    //protected abstract void RemoveWall(MazeCellData currentCell, MazeCellData chosenCell, CellDirections connectionDirection);

    protected abstract List<MazeCellData> GetUnvisitedNeighbours(MazeCellData currentCell, MazeDataGrid mazeGrid, Dictionary<uint, MazeCellData> visitedCells);

    protected bool TryGetNeighbour(IGridCoordinates neighbourCoordinates, MazeDataGrid grid, out MazeCellData neighbour)
    {
        if (grid.TryGetCell(neighbourCoordinates, out neighbour))
            return true;

        neighbour = null;
        return false;
    }

    protected bool CheckCoordinatesMatch(IGridCoordinates firstCoordinates, IGridCoordinates secondCoordinates) => firstCoordinates.X == secondCoordinates.X && firstCoordinates.Y == secondCoordinates.Y;
}
