using System.Collections;
using System.Collections.Generic;
using Bolt;
using UnityEngine;

public class EggController : Bolt.EntityBehaviour<IEggState>
{
    public float thrust = 5.0f;
    private float crackedness = 0.0f;
    private float crackThreshold = 100f;
    private Collision collision;
    private bool collided = false;
    private Rigidbody rb;
    public Transform followCam;
    public Player player;

    public override void Attached()
    {
        //Debug.Log("attaching");
        rb = GetComponent<Rigidbody>();
        //links the local transform to the state so it syncs
        state.SetTransforms(state.EggTransform, transform);
    }
    private void OnCollisionEnter(Collision collision)
    {
        //flag that a collision has occurred
        collided = true;
        this.collision = collision;
    }
    //process collision on server
    public override void SimulateOwner()
    {
        if (collided && !collision.gameObject.CompareTag("Soft"))
        {
            crackedness += collision.relativeVelocity.magnitude;
            state.crackedness = crackedness;
            BoltConsole.Write(crackedness.ToString());

            if (crackedness > crackThreshold)
            {
                TimerManager.instance.StartCoroutine(TimerManager.instance.Respawn(player, 4f));
                Destroy(this.gameObject);
            }

            collided = false;
        }
    }

    //runs on client
    public override void SimulateController()
    {
        //set up vectors to rotate around the player camera
        Vector3 forward = new Vector3(followCam.forward.x, 0, followCam.forward.z);
        forward.Normalize();
        Vector3 right = new Vector3(followCam.right.x, 0, followCam.right.z);
        right.Normalize();

        //create the input to send to the server
        IEggMoveInput input = EggMove.Create();
        input.Direction = Vector3.zero;
        //standard shitty unity if block
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

        //queue the command for execution on the server
        entity.QueueInput(input);

        Renderer renderer = GetComponent<Renderer>();
        Color colour = new Color(1.0f, 1.0f, 1.0f);
        renderer.material.SetColor("_EmissionColor", colour * (state.crackedness / crackThreshold));
    }

    //runs on both server and client cause ¯\_(ツ)_/¯
    public override void ExecuteCommand(Command command, bool resetState)
    {
        EggMove cmd = (EggMove)command;
        //Debug.Log(cmd);
        if (resetState)
        {
            //server disagrees with client so we reset to the server's view
            transform.position = cmd.Result.Position;
            rb.velocity = cmd.Result.Velocity;
        }
        else
        {
            //just push the player
            rb.AddForce(cmd.Input.Direction * thrust);
        }
        //return the new position and velocity from the server
        cmd.Result.Position = transform.position;
        cmd.Result.Velocity = rb.velocity;
        //probably pointless
        base.ExecuteCommand(command, resetState);
    }
}
