using UnityEngine;

public class RandomInput : IInput<Vector2Int>
{
    private Field _field;
    public bool HasData()
    {
        return true;
    }

    public Vector2Int Read()
    {
        var size = _field.Size();
        var coords = new Vector2Int(Random.Range(0, size.x), Random.Range(0, size.y));
        return coords;
    }
}

