using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[BoltGlobalBehaviour]
public class Network : Bolt.GlobalEventListener
{
    public override void SceneLoadLocalDone(string scene)
    {
        Vector3 spawnPos = new Vector3(Random.Range(-4, 4), 0, Random.Range(-4, 4));
        
        BoltEntity egh = BoltNetwork.Instantiate(BoltPrefabs.Egg, spawnPos, Quaternion.identity);
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");

        camera.GetComponent<CameraController>().target = egh.gameObject.transform;
        egh.GetComponent<EggController>().followCam = camera.transform;
        base.SceneLoadLocalDone(scene);
    }
}
