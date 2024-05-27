using UnityEngine;

public class Field : MonoBehaviour
{
    private Ship[] _ships;
    private FiledState[,] _field;
    private Vector2Int _size;

    public void Init(Ship[] ships, FiledState[,] field)
    {
        _ships = ships;
        _field = field;
        _size = new Vector2Int(field.GetLength(0), field.GetLength(1));
    }

    public Ship[] Ships()
    {
        return _ships;
    }

    public Vector2Int Size()
    {
        return _size;
    }

    public void TakeDamage(Vector2Int coords)
    {
        foreach (var ship in _ships)
        {
            if (ship.CheckForHit(coords))
            {
                ship.TakeDamage(coords);
                return;
            }
        }

        _field[coords.x, coords.y] = FiledState.Missed;
    }

    public Vector2Int ToFieldCoords(Vector3 coords)
    {
        // Пример перевода из мировых координат в координаты поля (для случая, если координаты совпадают)
        return new Vector2Int(Mathf.FloorToInt(coords.x), Mathf.FloorToInt(coords.z));
    }
}

