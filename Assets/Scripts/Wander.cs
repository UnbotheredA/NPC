using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : State
{
    // Start is called before the first frame update
    float distanceFromCircle = 3;
    float wanderSpeed = 3;

    Vector3 orginOfCircle;
    Vector3 randomPoint;

    public Wander(NPC npc, StateMachine stateMachine) : base(npc, stateMachine)
    {

    }
    public override void Enter()
    {
        //StartCoroutine(WaitBeforeNextPoint(10));
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }
    public IEnumerator WaitBeforeNextPoint(int waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        randomPoint = Random.insideUnitCircle * 5;
        orginOfCircle = npc.transform.position + (npc.transform.right.normalized * distanceFromCircle + randomPoint);
        Debug.DrawLine(npc.transform.position, randomPoint, Color.black);
        npc.targetPos = orginOfCircle * wanderSpeed * Time.deltaTime;
        npc.rb.MovePosition(npc.targetPos);
        yield return new WaitForSeconds(waitTime);
  
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public override void Exit()
    {
        base.Exit();
    }
    void waitForNewPoint()
    {
        randomPoint = Random.insideUnitCircle * 5;
        orginOfCircle = npc.transform.position + (npc.transform.right.normalized * distanceFromCircle + randomPoint);
        npc.targetPos = orginOfCircle * wanderSpeed * Time.deltaTime;
        npc.rb.MovePosition(npc.targetPos);
        Debug.DrawLine(npc.transform.position, randomPoint, Color.black);
    }
}
