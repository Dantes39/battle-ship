using UnityEngine;

public class Ship : MonoBehaviour
{
    private Vector2Int _size;
    private Vector2Int _position;
    private Vector2Int _bottomLeft;
    private Vector2Int _upperRight;
    private FiledState[,] _field;

    public void Init(Vector2Int position, Vector2Int size, FiledState[,] field)
    {
        _position = position;
        _bottomLeft = position;
        _size = size; 
        _upperRight = position + size - new Vector2Int(1, 1);
        _field = field;
    }

    public void Init() 
    {
        for (int x = _bottomLeft.x; x <= _upperRight.x; x++)
        {
            for (int y = _bottomLeft.y; y <= _upperRight.y; y++)
            {
                _field[x, y] = FiledState.ShipPart;
            }
        }
    }

    public Vector2Int Size()
    {
        return _size;
    }

    public bool CheckForHit(Vector2Int coords)
    {
        return coords.x >= _bottomLeft.x &&
               coords.y >= _bottomLeft.y &&
               coords.x <= _upperRight.x &&
               coords.y <= _upperRight.y;
    }

    public void TakeDamage(Vector2Int coords)
    {
        _field[coords.x, coords.y] = FiledState.Hited;
    }

    public bool IsAlive()
    {
        for (int x = _bottomLeft.x; x <= _upperRight.x; x++)
        {
            for (int y = _bottomLeft.y; y <= _upperRight.y; y++)
            {
                if (_field[x, y] == FiledState.ShipPart)
                    return true;
            }
        }

        return false;
    }
}

