using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[BoltGlobalBehaviour(BoltNetworkModes.Server, "Main")]
public class Network : Bolt.GlobalEventListener
{
    public override void SceneLoadRemoteDone(BoltConnection connection)
    {
        Debug.Log("remote load done");
        PlayerRegistry.GetPlayer(connection).Spawn();
        base.SceneLoadRemoteDone(connection);
    }

    public override void Connected(BoltConnection connection)
    {
        Debug.Log("connected");
        PlayerRegistry.CreatePlayer(connection);
        base.Connected(connection);
    }
}
