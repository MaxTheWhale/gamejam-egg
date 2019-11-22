using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[BoltGlobalBehaviour(BoltNetworkModes.Server, "Main")]
public class Network : Bolt.GlobalEventListener
{
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
