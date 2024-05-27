
using System.Collections;
using UnityEngine;

public class TurnState : MonoBehaviour, IState
{
    private IPlayer[] _player;

    public TurnState(IPlayer[] player)
    {
        _player = player;
    }

    public IEnumerator Enter()
    {
        foreach (var player in _player) 
        {
            var routine = player.DoTurn();
            while(routine.MoveNext())
                yield return routine.Current;
        }
    }
}

