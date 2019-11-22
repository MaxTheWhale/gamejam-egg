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
        Vector3 forward = new Vector3(followCam.forward.x, 0, followCam.forward.z);
        forward.Normalize();
        Vector3 right = new Vector3(followCam.right.x, 0, followCam.right.z);
        right.Normalize();
        Rigidbody rb = GetComponent<Rigidbody>();

        IEggMoveInput input = EggMove.Create();
        if (Input.GetKey(KeyCode.W))
        {
            input.Direction = forward * thrust;
        }
        if (Input.GetKey(KeyCode.A))
        {
            input.Direction = -right * thrust;
        }
        if (Input.GetKey(KeyCode.S))
        {
            input.Direction = -forward * thrust;
        }
        if (Input.GetKey(KeyCode.D))
        {
            input.Direction = right * thrust;
        }
        //Debug.Log("queueing input");
        entity.QueueInput(input);
    }

    public override void ExecuteCommand(Command command, bool resetState)
    {
        EggMove cmd = (EggMove)command;
        //Debug.Log(cmd);

        if (resetState)
        {
            transform.position = cmd.Result.Position;
            rb.velocity = cmd.Result.Velocity;
        }
        else
        {
            rb.AddForce(cmd.Input.Direction);
        }
        cmd.Result.Position = transform.position;
        cmd.Result.Velocity = rb.velocity;
        base.ExecuteCommand(command, resetState);
    }
}
