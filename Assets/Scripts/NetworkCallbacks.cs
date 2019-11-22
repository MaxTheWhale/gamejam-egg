using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[BoltGlobalBehaviour]
public class Network : Bolt.GlobalEventListener
{
    //private Dictionary<BoltConnection, BoltEntity> playerDict
    //{
    //    get
    //    {
    //        if (_playerDict == null)
    //        {
    //            _playerDict = new Dictionary<BoltConnection, BoltEntity>();
    //        }
    //        return _playerDict;
    //    }
    //}
    //static Dictionary<BoltConnection, BoltEntity> _playerDict = null;

    private void Awake()
    {
        //create the server player
        PlayerRegistry.CreatePlayer(null);
    }
    public override void SceneLoadLocalDone(string scene)
    {
        PlayerRegistry.serverPlayer.Spawn();
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
