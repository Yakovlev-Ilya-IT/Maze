using UnityEngine;

public class MazeCellContent : MonoBehaviour
{
    [SerializeField] private MazeCellContentType _type;

    public MazeCellContentType Type => _type;

    public void SpawnTo(Vector3 position)
    {
        transform.position = position;
    }

    public void Recycle()
    {
        Destroy(gameObject);
    }
}
