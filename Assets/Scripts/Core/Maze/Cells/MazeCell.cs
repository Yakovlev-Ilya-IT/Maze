using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MazeCell : MonoBehaviour
{
    [SerializeField] private MazeCellView _view;
    [SerializeField] private Transform _surfaceCenterPoint;

    private IGridCoordinates _gridCoordinates;
    private Dictionary<CellDirections, bool> _walls;
    private List<IGridCoordinates> _neighboursCoordinates;

    private MazeCellContent _content;

    public float CoordinatesConversionMultiplier => _view.FloorSizeForCoordinateConversion;
    public float SizeX => _view.FloorSizeX;
    public float SizeZ => _view.FloorSizeZ;

    public Dictionary<CellDirections, bool> Walls => _walls;
    public List<IGridCoordinates> NeighboursCoordinates => _neighboursCoordinates;
    public int Neighbours => _neighboursCoordinates.Count;

    public IGridCoordinates GridCoordinates => _gridCoordinates;
    public Vector3 SurfaceCenterPosition => _surfaceCenterPoint.position;

    public MazeCellContent Content
    {
        get => _content;
        set
        {
            if(_content != null)
                _content.Recycle();

            _content = value;
            _content.SpawnTo(SurfaceCenterPosition);
            _content.transform.SetParent(transform);
        }
    }

    public void Initialize(MazeCellData data, MazeCellContent content)
    {
        _gridCoordinates = data.Coordinates;
        _walls = data.Walls;
        //_neighboursCoordinates = data.NeighboursCoordinates;

        Content = content; 

        _view.Initialize(data);
    }

    public Vector2 GetScaledCartesianCoordinate()
    {
        float x, y;
        _gridCoordinates.ConvertToScaledCartesian(CoordinatesConversionMultiplier, out x, out y);

        return new Vector2(x, y);
    }

    public void SpawnTo(Vector3 position)
    {
        transform.position = position;
    }
}
