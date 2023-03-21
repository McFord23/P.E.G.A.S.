using Enums;
using UnityEngine;

public class Thermaldome : MonoBehaviour
{
    public PlayersManager players;

    void Update()
    {
        if (GetDistance(1) > 225) players.KillPlayer(1);
        if (Save.gameMode != GameMode.Single && GetDistance(2) > 225) players.KillPlayer(2);
    }

    float GetDistance(int player)
    {
        var vector = players.GetPlayerPosition(player) - transform.position;
        return Mathf.Abs(vector.magnitude);
    }
}
