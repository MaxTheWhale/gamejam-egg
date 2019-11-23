using UnityEngine;
using System.Collections.Generic;

public static class PlayerRegistry
{
    static List<Player> players = new List<Player>();

    public static Player CreatePlayer(BoltConnection connection)
    {
        Debug.Log("creating player");
        Player player = new Player();
        player.connection = connection;
        //store the player object in the connection user data to be propagated
        //no I'm not really sure why either
        player.connection.UserData = player;
        players.Add(player);
        return player;
    }
    public static Player GetPlayer(BoltConnection connection)
    {
        return (Player)connection.UserData;
    }
}

//link a physical entity to the client connection
public class Player
{
    public BoltEntity entity;
    public BoltConnection connection;
    public void Spawn()
    {
        if (!entity)
        {
            Debug.Log("spawning player");
            Vector3 spawnPos = new Vector3(0,10,0);

            entity = BoltNetwork.Instantiate(BoltPrefabs.Egg, spawnPos, Quaternion.identity);
            EggController controller = entity.GetComponent<EggController>();
            controller.player = this;
            entity.AssignControl(connection);
        }
    }
}
