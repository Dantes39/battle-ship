using UnityEngine;

public class CoordsInput : IInput<Vector2Int>
{
    private Field _field;
    public bool HasData()
    {
        return Input.GetKeyDown(KeyCode.Mouse0);
    }

    public Vector2Int Read()
    {
        var coords = Input.mousePosition;
        var realitiveCoords = _field.ToFieldCoords(coords);
        return realitiveCoords;
    }
}

