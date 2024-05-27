
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour, IPlayer
{
    private IInput<Vector2Int> _input;
    private Field _enemyField;

    public void Init(IInput<Vector2Int> input)
    {
        _input = input;
    }

    public IEnumerator DoTurn()
    {
        yield return new WaitWhile(() => !_input.HasData());
        var coords = _input.Read();
        _enemyField.TakeDamage(coords);
    }

    public void SetOpponentField(Field field)
    {
        _enemyField = field;
    }
}

