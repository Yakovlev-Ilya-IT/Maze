public class SquareCellFloorView : CellFloorView
{
    public override float SizeForCoordinateConversion => _meshRenderer.bounds.size.z;
}
