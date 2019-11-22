using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggController : MonoBehaviour
{
    public float thrust = 5.0f;
    private Rigidbody rb;
    public Transform followCam;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 forward = new Vector3(followCam.forward.x, 0, followCam.forward.z);
        forward.Normalize();
        Vector3 right = new Vector3(followCam.right.x, 0, followCam.right.z);
        right.Normalize();
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(forward * thrust);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(-right * thrust);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-forward * thrust);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(right * thrust);
        }
    }
}
