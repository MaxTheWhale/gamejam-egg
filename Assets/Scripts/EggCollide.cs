using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[BoltGlobalBehaviour(BoltNetworkModes.Server, "Main")]
public class EggCollide : Bolt.GlobalEventListener
{
    private float crackedness = 0.0f;

    private void OnCollisionEnter(Collision collision)
    {
        crackedness += collision.relativeVelocity.magnitude;
        BoltConsole.Write(crackedness.ToString());
    }
}
