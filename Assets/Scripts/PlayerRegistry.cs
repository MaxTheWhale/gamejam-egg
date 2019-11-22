using UnityEngine;
using System.Collections.Generic;

public static class PlayerRegistry
{
    static List<Player> players = new List<Player>();

    public static Player CreatePlayer(BoltConnection connection)
    {
        Player player = new Player();
        player.connection = connection;
        player.connection.UserData = player;
        players.Add(player);
        return player;
    }
    public static Player GetPlayer(BoltConnection connection)
    {
        return (Player)connection.UserData;
    }
}

public class Player
{
    public BoltEntity entity;
    public BoltConnection connection;
    public void Spawn()
    {
        if (!entity)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-4, 4), 0, Random.Range(-4, 4));

            entity = BoltNetwork.Instantiate(BoltPrefabs.Egg, spawnPos, Quaternion.identity);

            entity.AssignControl(connection);
        }
    }
}
