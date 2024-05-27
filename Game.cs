
using System.Collections;
using UnityEngine;

public class Game : MonoBehaviour
{
    private IState[] _states;

    public void Init(IState[] states)
    {
        _states = states;
    }

    public void Run() 
    {
        StartCoroutine(Loop());
    }

    private IEnumerator Loop()
    {
        for (int i = 0; i < _states.Length; i++)
        {
            var routine = _states[i].Enter();
            while (routine.MoveNext())
                yield return routine.Current;
        }
    }
}

