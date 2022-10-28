using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class CellFloorView : MonoBehaviour
{
    private MeshRenderer _meshRenderer;

    public float SizeX => _meshRenderer.bounds.size.x;
    public float SizeZ => _meshRenderer.bounds.size.z;

    public void Initialize()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }
}
