using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public abstract class CellFloorView : MonoBehaviour
{
    protected MeshRenderer _meshRenderer;

    public abstract float SizeForCoordinateConversion { get; }
    public virtual float SizeX => _meshRenderer.bounds.size.x;
    public virtual float SizeZ => _meshRenderer.bounds.size.z;

    public void Initialize()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }
}
