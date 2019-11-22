using System.Collections;
using System.Collections.Generic;
using Bolt;
using UnityEngine;

public class EggController : Bolt.EntityBehaviour<IEggState>
{
    public float thrust = 5.0f;
    private Rigidbody rb;
    public Transform followCam;

    public override void Attached()
    {
        Debug.Log("attaching");
        rb = GetComponent<Rigidbody>();
        state.SetTransforms(state.EggTransform, transform);
    }

    public override void SimulateController()
    {
        IEggMoveInput input = EggMove.Create();
        if (Input.GetKey(KeyCode.W))
        {
            input.Forward = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            input.Left = true;
            //rb.AddForce(-right * thrust);
        }
        if (Input.GetKey(KeyCode.S))
        {
            input.Backward = true;
            //rb.AddForce(-forward * thrust);
        }
        if (Input.GetKey(KeyCode.D))
        {
            input.Right = true;
            //rb.AddForce(right * thrust);
        }
        //Debug.Log("queueing input");
        entity.QueueInput(input);
    }

    public override void ExecuteCommand(Command command, bool resetState)
    {
        Vector3 forward = new Vector3(followCam.forward.x, 0, followCam.forward.z);
        forward.Normalize();
        Vector3 right = new Vector3(followCam.right.x, 0, followCam.right.z);
        right.Normalize();
        Rigidbody rb = GetComponent<Rigidbody>();

        EggMove cmd = (EggMove)command;
        //Debug.Log(cmd);

        if (resetState)
        {
            transform.position = cmd.Result.Position;
            rb.velocity = cmd.Result.Velocity;
        }
        if (cmd.Input.Forward)
        {
            rb.AddForce(forward * thrust);
        }
        if (cmd.Input.Left)
        {
            rb.AddForce(-right * thrust);
        }
        if (cmd.Input.Backward)
        {
            rb.AddForce(-forward * thrust);
        }
        if (cmd.Input.Right)
        {
            rb.AddForce(right * thrust);
        }
        cmd.Result.Position = transform.position;
        cmd.Result.Velocity = rb.velocity;
        base.ExecuteCommand(command, resetState);
    }
}
