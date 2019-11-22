using UnityEngine;
using System.Collections;

[BoltGlobalBehaviour("Main")]
public class PlayerCallbacks : Bolt.GlobalEventListener
{
    public override void ControlOfEntityGained(BoltEntity entity)
    {
        Debug.Log("control gained");
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");

        camera.GetComponent<CameraController>().target = entity.gameObject.transform;
        entity.GetComponent<EggController>().followCam = camera.transform;

        base.ControlOfEntityGained(entity);
    }
}
