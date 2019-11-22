using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[BoltGlobalBehaviour(BoltNetworkModes.Server)]
public class Boing : MonoBehaviour
{
    public float force = 600;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collider hit");
        Rigidbody hit = collision.collider.gameObject.GetComponent<Rigidbody>();
        if (hit != null)
        {
            Debug.Log(hit);
            hit.AddForce(new Vector3(0, force, 0));
        }
    }
}
