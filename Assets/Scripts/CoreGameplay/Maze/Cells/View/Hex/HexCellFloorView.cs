public class HexCellFloorView : CellFloorView
{
    private const float OffsetBetweenCellsByY = 3f / 4f;

    public override float SizeForCoordinateConversion => _meshRenderer.bounds.size.z / 2f;

    public override float SizeZ => base.SizeZ * OffsetBetweenCellsByY;
}
