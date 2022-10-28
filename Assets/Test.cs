using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Cell _cellPrefab;

    public GridName GridName;

    public int Width = 15;
    public int Height = 15;

    public void Start()
    {
        IMazeGridGenerator mazeGridGenerator;
        if (GridName == GridName.Square)
        {
            mazeGridGenerator = new DepthFirstSearchMazeSquareGridGenerator();
        }
        else
        {
            mazeGridGenerator = new DepthFirstSearchMazeHexGridGenerator();
        }

        Grid grid = mazeGridGenerator.Generate(Width, Height);

        for (int i = 0; i < grid.CellsData.GetLength(0); i++)
        {
            for (int j = 0; j < grid.CellsData.GetLength(1); j++)
            {
                if(j%2 == 0)
                {
                    Cell cell = Instantiate(_cellPrefab, new Vector3(2 * grid.CellsData[i, j].Coordinates.ConvertToGridCartesian().X, 0, 1.73f * grid.CellsData[i, j].Coordinates.ConvertToGridCartesian().Y), Quaternion.identity, gameObject.transform);
                    cell.Initialize(grid.CellsData[i, j]);
                }
                else
                {
                    Cell cell = Instantiate(_cellPrefab, new Vector3(2 * grid.CellsData[i, j].Coordinates.ConvertToGridCartesian().X - 1f, 0, 1.73f * grid.CellsData[i, j].Coordinates.ConvertToGridCartesian().Y), Quaternion.identity, gameObject.transform);
                    cell.Initialize(grid.CellsData[i, j]);
                }
            }
        }
    }

}

public enum GridName{
    Hex,
    Square
}
