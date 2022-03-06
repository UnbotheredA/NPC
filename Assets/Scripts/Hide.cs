using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : State
{
    GameObject closeRangeNPC;
    GameObject target;
    GameObject currentObstacle;
    Rigidbody2D rbody;

    GameObject[] obstacles;

    Vector3 shortestDistaceToObstacle;
    Vector3 oppositeDirectionOfPlayer;
    public Hide(NPC npc, StateMachine stateMachine) : base(npc, stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        target = GameObject.Find("Trigger");
        closeRangeNPC = GameObject.Find("OfficalCloseRangeNPC");
        rbody = closeRangeNPC.GetComponent<Rigidbody2D>();
        obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        shortestDistaceToObstacle = new Vector3(-1000, -1000, 0);
        oppositeDirectionOfPlayer = new Vector3(1000, 1000, 1000);
        closeRangeNPC.transform.rotation = Quaternion.Inverse(target.transform.rotation);
        Debug.Log("Hide State");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        for (int i = 0; i < obstacles.Length; i++)
        {
            Vector3 distanceToObstacle = closeRangeNPC.transform.position - obstacles[i].transform.position;
            //TODO && camera not in sight and opposite direction of the player 
            if (distanceToObstacle.magnitude < shortestDistaceToObstacle.magnitude)
            {
                shortestDistaceToObstacle = closeRangeNPC.transform.position - obstacles[i].transform.position;
                currentObstacle = obstacles[i];
                Debug.Log(currentObstacle.name);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (rbody && currentObstacle && target != null)
        {
            rbody.MovePosition(rbody.transform.position + currentObstacle.transform.position * npc.speed * Time.deltaTime);
        }
    }
}
