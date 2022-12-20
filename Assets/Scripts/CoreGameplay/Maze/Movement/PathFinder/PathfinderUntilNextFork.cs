using System;
using System.Collections.Generic;

public class PathfinderUntilNextFork : IMovementPathfinder
{
    private IMazeGrid _mazeGrid;
    private const int NumberOpenNeighbourCellForMovement = 2;

    public PathfinderUntilNextFork(IMazeGrid mazeGrid)
    {
        _mazeGrid = mazeGrid;
    }

    public Queue<MazeCell> CalculatePath(MazeCell startCell, CellDirections direction)
    {
        Queue<MazeCell> path = new Queue<MazeCell>();

        MazeCell previousCell = startCell;
        MazeCell currentCell;

        if (_mazeGrid.TryGetCell(previousCell.DirectionToNeighboursCoordinates[direction], out MazeCell cell))
            currentCell = cell;
        else
            throw new ArgumentException("Error direction");

        path.Enqueue(currentCell);

        while (currentCell.Neighbours == NumberOpenNeighbourCellForMovement)
        {
            foreach (KeyValuePair<CellDirections, IGridCoordinates> coordinates in currentCell.DirectionToNeighboursCoordinates)
            {
                if (_mazeGrid.TryGetCell(coordinates.Value, out MazeCell nextCell))
                {
                    if (nextCell == previousCell)
                        continue;

                    path.Enqueue(nextCell);
                    previousCell = currentCell;
                    currentCell = nextCell;
                    break;
                }
                else
                {
                    throw new Exception("Error coordinates");
                }
            }
        }

        return path;
    }
}
