using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : State
{
    private Transform target;
    public Chase(NPC npc, StateMachine stateMachine) : base(npc, stateMachine)
    {

    }

    public override void Enter()
    {
        //Animation is handled here
        base.Enter();
        npc.speed = npc.chaseSpeed;
        target = GameObject.Find("Trigger").transform;
    }
    //basically since the close range npc is the only npc that gets to the player its postion will be 3 but the far range will still be patrolling
    //but when attack is true the far range will shoot and rotate towards the players pos because the shoot method is called on the phyics update of attack state
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (npc && target && stateMachine != null)
        {
            float distance = Vector3.Distance(npc.transform.position, target.transform.position);
            if (distance < 3f)
            {
                stateMachine.ChangeState(npc.attack);
            }
            //if we did not have this npc will only go back to patrol if attack state is active
            else if (distance > 10)
            {
                Debug.Log(npc.name + "is further than 10 so needs to go patrol");
                stateMachine.ChangeState(npc.patrol);
            }
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (target && npc && npc.rb != null)
        {
            //face the player when chasing the player
            Vector3 playerDirection = (target.transform.position - npc.transform.position).normalized;
            npc.targetPos = npc.transform.position + playerDirection * npc.speed * Time.deltaTime;
            npc.rb.MovePosition(npc.targetPos);
            float angle = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;
            Quaternion rotationOfPlayer = Quaternion.AngleAxis(angle, Vector3.forward);
            npc.rb.transform.rotation = rotationOfPlayer;
        }
    }
}
