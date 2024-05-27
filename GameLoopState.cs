
using System.Collections;
using UnityEngine;

public class GameLoopState : IState
{
    private GameObject[] _players;
    private IState _child;

    public GameLoopState(GameObject[] players, IState child)
    {
        _players = players;
        _child = child;
    }

    public IEnumerator Enter()
    {
        while (ShouldGameContinue())
        {
            var routine = _child.Enter();
            while(routine.MoveNext() && ShouldGameContinue()) 
                yield return routine.Current;
        }
    }

    private bool ShouldGameContinue()
    {
        var shouldContinue = true;

        foreach (var player in _players)
        {
            var playerFieldBuilderComponent = player.GetComponent<FieldBuilder>();
            var field = playerFieldBuilderComponent.GetField();


            shouldContinue &= CheckField(field);

        }

        return shouldContinue;
    }

    private bool CheckField(Field field)
    {
        foreach (var ship in field.Ships())
        {
            if (ship.IsAlive())
                return true;
        }

        return false;
    }
}

