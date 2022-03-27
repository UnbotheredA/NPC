using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Hide : State
{
    GameObject closeRangeNPC;
    GameObject target;
    GameObject currentObstacle;
    //Rigidbody2D rbody;

    GameObject[] obstacles;
    List<GameObject> availableObstacles;

    //Vector3 shortestDistaceToObstacle;
    Vector3 hidePos;
    int hideDirection;
    public Hide(NPC npc, StateMachine stateMachine) : base(npc, stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        npc.speed = npc.hideSpeed;
        target = GameObject.Find("Trigger");
        closeRangeNPC = GameObject.Find("OfficalCloseRangeNPC");
        obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        currentObstacle = null;
        Debug.Log("Hide State");

        availableObstacles = obstacles.ToList();
        availableObstacles.Remove(FindNearestObject(availableObstacles.ToArray()));
        currentObstacle = FindNearestObject(availableObstacles.ToArray());
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        //hidePos = FindHideSpot(FindClosestGameObject(obstacles));
        ///Debug.DrawLine(target.transform.position, hidePos, Color.red);
        ///
        // finds the closet obstacle so later the npc can move to it
        //List<GameObject> availableObstacles = obstacles.ToList();


        if (currentObstacle != null)
        {
            float dist = Vector3.Distance(currentObstacle.transform.position, target.transform.position);

            if (dist < 6)
            {
                Enter();
            }
        }

        //currentObstacle = FindNearestObject(availableObstacles.ToArray());

        Debug.DrawLine(npc.transform.position, currentObstacle.transform.position, Color.white);
        float distanceFromColider = 4f;
        // the point between the current obstacle and the player and making sure to always be further from the target
        Vector3 direction = currentObstacle.transform.position - target.transform.position;
        direction.Normalize();
        // to go to the current obstacle and move away from player while keeping the distance from collider
        hidePos = currentObstacle.transform.position + direction * distanceFromColider;
        Debug.DrawLine(target.transform.position, hidePos, Color.red);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (npc.rb && currentObstacle && target != null)
        {
            Vector3 newPos = (hidePos - npc.transform.position).normalized;

            //instead of hide pos it is target
            bool right = Vector2.Dot(target.transform.position - npc.rb.transform.position, -npc.rb.transform.up) > 0;
            bool left = Vector2.Dot(target.transform.position - npc.rb.transform.position, -npc.rb.transform.up) < 0;

            if (left)
            {
                hideDirection = 1;
            }
            else if (right)
            {
                hideDirection = -1;
            }
            else
            {
                hideDirection = 0;
            }

            //Debug.LogError(hideDirection);


            float angle = Mathf.Atan2(newPos.y, newPos.x) * Mathf.Rad2Deg;
            Quaternion rotationOfSpot = Quaternion.AngleAxis(angle, Vector3.forward);
            //npc.rb.transform.rotation = Quaternion.Slerp(npc.rb.transform.rotation, rotationOfSpot, Time.deltaTime * 12);
            npc.rb.transform.rotation = rotationOfSpot;

            if (Vector3.Distance(npc.transform.position, hidePos) > 0.5f)
            {
                if (Vector3.Distance(npc.transform.position, currentObstacle.transform.position) > 4)
                {

                    npc.rb.MovePosition(npc.transform.position + newPos * npc.speed * Time.deltaTime);
                }
                else
                {
                    npc.rb.transform.position = RotatePointAroundPivot(npc.rb.transform.position, currentObstacle.transform.position, Quaternion.Euler(0, 0, hideDirection));

                }
            }
            else
            {
                npc.rb.velocity = Vector2.zero;
                npc.rb.constraints = RigidbodyConstraints2D.FreezePositionX;
                npc.rb.constraints = RigidbodyConstraints2D.FreezePositionY;
                npc.rb.constraints = RigidbodyConstraints2D.FreezeRotation;

            }
        }

    }
    //Make it so that if a tree is occupied the NPC needs to go to another one
    public static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Quaternion angle)
    {
        return angle * (point - pivot) + pivot;
    }

    private Vector3 FindHideSpot(Transform obs)
    {
        //Debug.DrawLine(npc.transform.position, obs.position, Color.white);
        //Debug.DrawLine(target.transform.position,currentObstacle.transform.position,Color.red);
        float distanceFromColider = 7f;
        //Debug.Log(obs.position);
        Vector3 direction = obs.position - target.transform.position;
        direction.Normalize();
        return obs.position + direction * distanceFromColider;
    }

    // find nearest object
    private GameObject FindNearestObject(GameObject[] allObjects)
    {
        GameObject nearest = null;
        float minDist = Mathf.Infinity;

        for (int i = 0; i < allObjects.Length; i++)
        {
            Vector3 distanceToObstacle = closeRangeNPC.transform.position - allObjects[i].transform.position;

            if (distanceToObstacle.sqrMagnitude < minDist)
            {
                minDist = (closeRangeNPC.transform.position - allObjects[i].transform.position).sqrMagnitude;
                nearest = allObjects[i];
                //Debug.Log(currentObstacle.name);
            }
        }
        return nearest;
    }

    // find furtherst object
    private GameObject FindFurthestObject(GameObject[] allObjects)
    {
        GameObject furthest = null;
        float maxDist = 0;

        for (int i = 0; i < allObjects.Length; i++)
        {
            Vector3 distanceToObstacle = closeRangeNPC.transform.position - allObjects[i].transform.position;

            if (distanceToObstacle.sqrMagnitude > maxDist)
            {
                maxDist = (closeRangeNPC.transform.position - allObjects[i].transform.position).sqrMagnitude;
                furthest = allObjects[i];
                //Debug.Log(currentObstacle.name);
            }
        }
        return furthest;
    }
}