
using System.Collections;
using UnityEngine;

public class PrepareFieldState : MonoBehaviour, IState
{
    private IFieldBuilder[] _fieldBuilders;

    public PrepareFieldState(IFieldBuilder[] fieldBuilders)
    {
        _fieldBuilders = fieldBuilders;
    }

    public IEnumerator Enter()
    {
        foreach (var player in _fieldBuilders)
        {
            var routine = player.FillField();
            while (routine.MoveNext())
                yield return routine.Current;
        }
    }

    public void Init(IFieldBuilder[] fieldBuilders)
    {
        _fieldBuilders = fieldBuilders;
    }

}

