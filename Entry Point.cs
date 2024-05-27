using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ObjectChangeEventStream;


public class EntryPoint : MonoBehaviour
{
    [SerializeField]
    private ShipFactory _shipFactory;

    [SerializeField]
    private Game _game;

    private GameObject _playerPrefab;
    private GameObject _aiPrefab;
    private void Start()
    {
        var alowedShips = new Dictionary<int, int>()
        {
            { 1, 4 },
            { 2, 3 },
            { 3, 2 },
            { 4, 1 },
        };

        var ai = Instantiate(_playerPrefab);
        var aiBuilder = ai.GetComponent<FieldBuilder>();
        var aiPlayerComponent = ai.GetComponent<Player>();
        aiBuilder.Init(_shipFactory, new RandomInput(), alowedShips, 10);

        var player = Instantiate(_playerPrefab);
        var playerBuilder = player.GetComponent<FieldBuilder>();
        var playerPlayerComponent = ai.GetComponent<Player>();
        playerBuilder.Init(_shipFactory, new RandomInput(), alowedShips, 10);

        var playerObjects = new GameObject[] { ai, player };
        var players = new IPlayer[] { aiPlayerComponent, playerPlayerComponent };

        var builders = new IFieldBuilder[] { aiBuilder, playerBuilder };

        var prepareState = new PrepareFieldState(builders);
        var turnState = new TurnState(players);
        var loopState = new GameLoopState(playerObjects, turnState);
        var endGameState = new EndGameState();

        var states = new IState[] { prepareState, loopState, endGameState };

        _game.Init(states);
        _game.Run();
    }
}

