using System.Collections.Generic;
using UnityEngine;

public abstract class CellView : MonoBehaviour
{
    [SerializeField] protected CellFloorView _floor;
    protected Dictionary<CellDirections, CellWallView> _walls;

    public CellFloorView Floor => _floor;
    public Dictionary<CellDirections, CellWallView> Walls => _walls;

    public float FloorSizeX => _floor.SizeX;
    public float FloorSizeZ => _floor.SizeZ;

    public virtual void Initialize(CellData data)
    {
        _floor.Initialize();
    }

    protected void SetupWalls(CellData data)
    {
        foreach (KeyValuePair<CellDirections, CellWallView> wall in _walls)
        {
            if (data.Walls.ContainsKey(wall.Key))
            {
                if (data.Walls[wall.Key])
                {
                    wall.Value.Show();
                    continue;
                }

                wall.Value.Hide();
            }
            else
            {
                Debug.LogError("no wall needed");
            }
        }
    }
}
