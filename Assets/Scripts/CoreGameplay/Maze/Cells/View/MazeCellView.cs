using System.Collections.Generic;
using UnityEngine;

public abstract class MazeCellView : MonoBehaviour
{
    [SerializeField] private CellFloorView _floor;
    private Dictionary<CellDirections, CellWallView> _walls;

    public float FloorSizeForCoordinateConversion => _floor.SizeForCoordinateConversion;
    public float FloorSizeX => _floor.SizeX;
    public float FloorSizeZ => _floor.SizeZ;

    public virtual void Initialize(MazeCellData data)
    {
        SetupWalls(data);
        _floor.Initialize();
    }

    private void SetupWalls(MazeCellData data)
    {
        _walls = GetWalls();

        foreach (KeyValuePair<CellDirections, CellWallView> wall in _walls)
        {
            if (data.DirectionToNeighboursCoordinates.ContainsKey(wall.Key))
            {
                wall.Value.Hide();
            }
            else
            {
                wall.Value.Show();
            }
        }
    }

    protected abstract Dictionary<CellDirections, CellWallView> GetWalls();
}
