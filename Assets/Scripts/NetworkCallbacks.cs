using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[BoltGlobalBehaviour]
public class Network : Bolt.GlobalEventListener
{
    public override void SceneLoadLocalDone(string scene)
    {
        Vector3 spawnPos = new Vector3(Random.Range(-4, 4), Random.Range(-4, 4));
        BoltNetwork.Instantiate(BoltPrefabs.Egg, spawnPos, Quaternion.identity);
        base.SceneLoadLocalDone(scene);
    }
}
