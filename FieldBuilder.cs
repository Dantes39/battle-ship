
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldBuilder : MonoBehaviour, IFieldBuilder
{
    private ShipFactory _shipFactory;
    private IInput<Vector2Int> _input;
    private Dictionary<int, int> _allowedShips; // ключ - размер корабля, значение - количество кораблей такого размера
    private Field _field;
    private int _fieldSize = 10; // Размер поля 10x10

    public void Init(ShipFactory shipFactory, IInput<Vector2Int> input, Dictionary<int, int> allowedShips, int fieldSize)
    {
        _shipFactory = shipFactory;
        _input = input;
        _allowedShips = allowedShips;
        _fieldSize = fieldSize;
    }

    private void Start()
    {
        // Пример инициализации
        _allowedShips = new Dictionary<int, int> { { 4, 1 }, { 3, 2 }, { 2, 3 }, { 1, 4 } };
        _fieldSize = 10;
        StartCoroutine(FillField());
    }

    public IEnumerator FillField()
    {
        _field = new Field();
        var grid = new FiledState[_fieldSize, _fieldSize];

        var ships = new List<Ship>();

        foreach (var shipInfo in _allowedShips)
        {
            var shipSize = shipInfo.Key;
            var shipCount = shipInfo.Value;

            for (int i = 0; i < shipCount; i++)
            {
                bool placed = false;

                while (!placed)
                {
                    var startPos = new Vector2Int(Random.Range(0, _fieldSize), Random.Range(0, _fieldSize));
                    var size = new Vector2Int(shipSize, 1);
                    var isHorizontal = Random.Range(0, 2) == 0;
                    if (!isHorizontal)
                    {
                        size = new Vector2Int(1, shipSize);
                    }

                    if (CanPlaceShip(startPos, size, grid))
                    {
                        var newShip = _shipFactory.Create(shipSize);
                        newShip.Init(startPos, size, grid);
                        newShip.Init();
                        ships.Add(newShip);
                        placed = true;
                    }

                    yield return null; // Ожидание следующего кадра
                }
            }
        }

        _field.Init(ships.ToArray(), grid);
    }

    private bool CanPlaceShip(Vector2Int startPos, Vector2Int size, FiledState[,] grid)
    {
        
        for (int x = startPos.x - 1; x <= startPos.x + size.x; x++)
        {
            for (int y = startPos.y - 1; y <= startPos.y + size.y; y++)
            {
                if (x >= 0 && x < _fieldSize && y >= 0 && y < _fieldSize)
                {
                    if (grid[x, y] != FiledState.Empty) 
                        return false;
                }
            }
        }

        return true;
    }

    public Field GetField()
    {
        return _field;
    }
}

