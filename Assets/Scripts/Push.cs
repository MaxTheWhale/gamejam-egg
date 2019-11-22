using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour
{
    private void OnTriggerStay(Collider collider)
    {
        Debug.Log("Trigger triggered");
        Rigidbody rb = collider.gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = new Vector3(4, rb.velocity.y, rb.velocity.z);
        }
    }
}
