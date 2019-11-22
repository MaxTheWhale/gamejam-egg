using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggController : MonoBehaviour
{
    public float thrust;
    public Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            rb.AddForce(Vector3.forward * thrust);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            rb.AddForce(-Vector3.right * thrust);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            rb.AddForce(-Vector3.forward * thrust);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            rb.AddForce(Vector3.right * thrust);
        }
    }
}
