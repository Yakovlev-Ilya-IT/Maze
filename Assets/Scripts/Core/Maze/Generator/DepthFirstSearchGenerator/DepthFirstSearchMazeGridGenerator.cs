using System.Collections.Generic;
using UnityEngine;

public abstract class DepthFirstSearchMazeGridGenerator : IMazeGridGenerator
{
    protected Dictionary<uint, CellData> _visitedCells = new Dictionary<uint, CellData>();

    public Grid Generate(int width, int height)
    {
        Grid mazeGrid = BuildInitialGrid(width, height);

        GenerateMaze(mazeGrid);

        return mazeGrid;

    }

    protected abstract Grid BuildInitialGrid(int width, int height);

    private void GenerateMaze(Grid mazeGrid)
    {
        CellData currentCell = mazeGrid.GetRandomCell();

        _visitedCells.Add(currentCell.Id, currentCell);

        Stack<CellData> mazeCells = new Stack<CellData>();

        do
        {
            List<CellData> unvisitedNeighbours = GetUnvisitedNeighbours(currentCell, mazeGrid);

            if (unvisitedNeighbours.Count > 0)
            {
                CellData chosenCell = unvisitedNeighbours[Random.Range(0, unvisitedNeighbours.Count)];

                RemoveWall(currentCell, chosenCell);

                _visitedCells.Add(chosenCell.Id, chosenCell);
                mazeCells.Push(chosenCell);
                currentCell = chosenCell;
            }
            else
            {
                currentCell = mazeCells.Pop();
            }
        } while (mazeCells.Count > 0);
    }

    protected abstract void RemoveWall(CellData currentCell, CellData chosenCell);

    protected abstract List<CellData> GetUnvisitedNeighbours(CellData currentCell, Grid mazeGrid);

    protected bool TryGetNeighbour(IGridCoordinates neighbourCoordinates, Grid grid, out CellData neighbour)
    {
        if (grid.TryGetCell(neighbourCoordinates, out neighbour))
            return true;

        neighbour = null;
        return false;
    }

    protected bool CheckCoordinatesMatch(IGridCoordinates firstCoordinates, IGridCoordinates secondCoordinates) => firstCoordinates.X == secondCoordinates.X && firstCoordinates.Y == secondCoordinates.Y;
}
