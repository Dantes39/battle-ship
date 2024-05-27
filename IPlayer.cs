
using System.Collections;

public interface IPlayer
{
    void SetOpponentField(Field field);
    IEnumerator DoTurn();
}

