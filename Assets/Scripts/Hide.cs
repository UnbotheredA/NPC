using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : State
{
    GameObject closeRangeNPC;
    GameObject target;
    GameObject currentObstacle;
    //Rigidbody2D rbody;

    GameObject[] obstacles;

    Vector3 shortestDistaceToObstacle;
    Vector3 oppositeDirectionOfPlayer;
    Vector3 hidePos;
    public Hide(NPC npc, StateMachine stateMachine) : base(npc, stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        target = GameObject.Find("Trigger");
        closeRangeNPC = GameObject.Find("OfficalCloseRangeNPC");
        //rbody = closeRangeNPC.GetComponent<Rigidbody2D>();
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
        hidePos = FindHideSpot(FindClosestGameObject(obstacles));
        Debug.DrawLine(target.transform.position, hidePos, Color.red);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (npc.rb && currentObstacle && target != null)
        {
            //face the hiding spot when hiding from the player
            Vector3 spotDirection = (hidePos - npc.transform.position).normalized;
            npc.rb.MovePosition(npc.transform.position + spotDirection * npc.speed * Time.deltaTime);
            float angle = Mathf.Atan2(spotDirection.y, spotDirection.x) * Mathf.Rad2Deg;
            Quaternion rotationOfSpot = Quaternion.AngleAxis(angle, Vector3.forward);
            npc.rb.transform.rotation = rotationOfSpot;
        }
    }
    private Vector3 FindHideSpot(Transform obs)
    {
        Debug.DrawLine(npc.transform.position, obs.position, Color.white);
        //Debug.DrawLine(target.transform.position,currentObstacle.transform.position,Color.red);
        float distanceFromColider = 7f;
        Vector3 direction = obs.position - target.transform.position;
        direction.Normalize();
        return obs.position + direction * distanceFromColider;
    }

    private Transform FindClosestGameObject(GameObject[] objs) 
    {

        Transform closestObstacle = null;
        float minDist = Mathf.Infinity;

        for (int i = 0; i < objs.Length; i++)
        {
            Vector3 distanceToObstacle = closeRangeNPC.transform.position - objs[i].transform.position;

            //TODO && camera not in sight and opposite direction of the player 
            if (distanceToObstacle.magnitude < shortestDistaceToObstacle.magnitude)
            {
                shortestDistaceToObstacle = closeRangeNPC.transform.position - objs[i].transform.position;
                closestObstacle = objs[i].transform;
                Debug.Log(closestObstacle.name);
            }
        }
        return closestObstacle;
    }
}
