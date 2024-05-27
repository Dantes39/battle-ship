
using System.Collections;
using UnityEngine;

public class ShareFieldState : MonoBehaviour, IState
{
    private GameObject[] _player;

    public IEnumerator Enter()
    {
        for (int i = 0; i < _player.Length; i++)
        {
            var player = _player[i];
            var nextPlayer = _player[(i + 1) % _player.Length];

            var nextPlayerComponent = nextPlayer.GetComponent<Player>();
            var playerFieldBuilderComponent = player.GetComponent<FieldBuilder>();

            var field = playerFieldBuilderComponent.GetField();
            nextPlayerComponent.SetOpponentField(field);
        }

        yield return null;
    }
}

