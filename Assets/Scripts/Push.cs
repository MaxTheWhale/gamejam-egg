using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour
{
    private void OnCollisionStay(Collision collision)
    {
        Rigidbody rb = collision.collider.gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(new Vector3(-50, 0, 0));
        }
    }
}
