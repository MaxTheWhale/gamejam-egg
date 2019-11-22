using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[BoltGlobalBehaviour("Main")]
public class Network : Bolt.GlobalEventListener
{
    private void Awake()
    {
        //create the server player
        PlayerRegistry.CreatePlayer(null);
    }
    public override void SceneLoadLocalDone(string scene)
    {
        //if (PlayerRegistry.serverPlayer.entity == null)
        //{
        //    PlayerRegistry.serverPlayer.Spawn();
        //}
        base.SceneLoadLocalDone(scene);
    }

    public override void SceneLoadRemoteDone(BoltConnection connection)
    {
        PlayerRegistry.GetPlayer(connection).Spawn();
        base.SceneLoadRemoteDone(connection);
    }

    public override void Connected(BoltConnection connection)
    {
        PlayerRegistry.CreatePlayer(connection);
        base.Connected(connection);
    }
}
