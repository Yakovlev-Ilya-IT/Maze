using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] CellView _view;

    public float SizeX => _view.FloorSizeX;
    public float SizeZ => _view.FloorSizeZ;

    public void Initialize(CellData data)
    {
        _view.Initialize(data);
    }
}
