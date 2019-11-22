using System.Collections;
using System.Collections.Generic;
using Bolt;
using UnityEngine;

public class EggController : Bolt.EntityBehaviour<IEggState>
{
    public float thrust = 5.0f;
    private float crackedness = 0.0f;
    private Collision collision;
    private bool collided = false;
    private Rigidbody rb;
    public Transform followCam;

    public override void Attached()
    {
        Debug.Log("attaching");
        rb = GetComponent<Rigidbody>();
        state.SetTransforms(state.EggTransform, transform);
    }

    public override void SimulateOwner()
    {
        if (collided)
        {
            crackedness += collision.relativeVelocity.magnitude;
            BoltConsole.Write(crackedness.ToString());
            collided = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        collided = true;
        this.collision = collision;
    }
    public override void SimulateController()
    {
        Vector3 forward = new Vector3(followCam.forward.x, 0, followCam.forward.z);
        forward.Normalize();
        Vector3 right = new Vector3(followCam.right.x, 0, followCam.right.z);
        right.Normalize();
        Rigidbody rb = GetComponent<Rigidbody>();

        IEggMoveInput input = EggMove.Create();
        input.Direction = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            input.Direction += forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            input.Direction += -right;
        }
        if (Input.GetKey(KeyCode.S))
        {
            input.Direction += -forward;
        }
        if (Input.GetKey(KeyCode.D))
        {
            input.Direction += right;
        }
        input.Direction.Normalize();
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
            rb.AddForce(cmd.Input.Direction * thrust);
        }
        cmd.Result.Position = transform.position;
        cmd.Result.Velocity = rb.velocity;
        base.ExecuteCommand(command, resetState);
    }
}
