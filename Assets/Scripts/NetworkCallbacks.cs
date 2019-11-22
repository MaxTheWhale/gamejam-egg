using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[BoltGlobalBehaviour(BoltNetworkModes.Server, "Main")]
public class Network : Bolt.GlobalEventListener
{
    //a client has finished loading the scene
    public override void SceneLoadRemoteDone(BoltConnection connection)
    {
        //Debug.Log("remote load done");
        //spawn in the player's character
        PlayerRegistry.GetPlayer(connection).Spawn();
        base.SceneLoadRemoteDone(connection);
    }

    //a client has connected
    public override void Connected(BoltConnection connection)
    {
        //Debug.Log("connected");
        PlayerRegistry.CreatePlayer(connection);
        base.Connected(connection);
    }
}
