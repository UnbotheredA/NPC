using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Hide : State
{
    GameObject target;
    GameObject currentObstacle;
    //Rigidbody2D rbody;

    GameObject[] obstacles;
    List<GameObject> availableObstacles;

    //Vector3 shortestDistaceToObstacle;
    Vector3 hidePos;
    int hideDirection;
    float distanceFromColider = 4f;
    float timerTilBail;
    public Hide(NPC npc, StateMachine stateMachine) : base(npc, stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        npc.speed = npc.hideSpeed;
        target = GameObject.Find("Trigger");
        obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        currentObstacle = null;
        Debug.Log("Hide State");
        availableObstacles = obstacles.ToList();
        availableObstacles.Remove(UtlityFunctions.FindNearestObject(availableObstacles.ToArray(), npc.gameObject));
        currentObstacle = UtlityFunctions.FindNearestObject(availableObstacles.ToArray(), npc.gameObject);
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();
        npc.health += 1 * Time.deltaTime;
        if (npc.health >= 10)
        {
            Debug.Log(npc.health);
            Debug.Log("npc health is now back to good");
            stateMachine.ChangeState(npc.patrol);
        }
        if (currentObstacle != null)
        {
            float dist = Vector3.Distance(currentObstacle.transform.position, target.transform.position);

            if (dist < 6)
            {
                timerTilBail += Time.deltaTime;

                if (timerTilBail >= npc.bailTime || dist < 3)
                    Enter();
            }
            else
            {
                if (timerTilBail > 0)
                    timerTilBail = 0;
            }
        }

        Debug.DrawLine(npc.transform.position, currentObstacle.transform.position, Color.white);
        //Args are the player it is hiding away from the current ob that it is using to hide and
        hidePos = UtlityFunctions.FindSpotBehind(target, currentObstacle, distanceFromColider);
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
            float angle = Mathf.Atan2(newPos.y, newPos.x) * Mathf.Rad2Deg;
            Quaternion rotationOfSpot = Quaternion.AngleAxis(angle, Vector3.forward);
            npc.rb.transform.rotation = rotationOfSpot;
            if (Vector3.Distance(npc.transform.position, hidePos) > 0.5f)
            {
                npc.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(hidePos);
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
    public static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Quaternion angle)
    {
        return angle * (point - pivot) + pivot;
    }
    // The first parameter would be the NPC and the second would be the object it hides from
    public override void Exit()
    {
        base.Exit();
        //npc.stopAI();
    }
}


