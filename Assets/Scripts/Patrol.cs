using UnityEngine;

public class Patrol : State
{

    public Waypoint waypoint;

    private Transform target;
    //if player is out of sight for 10 seconds go to patrol
    private GameObject closeRangeNPC;

    private GameObject farRangeNPC;

    private Rigidbody2D rigidbody2D1;

    //TODO make it so that if the npc is returing from attack or chase state to not use patrol speed but npc default speed
    public Patrol(NPC npc, StateMachine stateMachine) : base(npc, stateMachine)
    {
        //check if a spesfic npcs is a spesfic state and if so do this.
    }
    public override void Enter()
    {
        base.Enter();
        npc.speed = 1;
        farRangeNPC = GameObject.Find("FarRangeNPC");
        closeRangeNPC = GameObject.Find("OfficalCloseRangeNPC");
        target = GameObject.Find("Trigger").transform;
        //this line is causing the problem.
        //closeRangeNPC.GetComponent<BoxCollider2D>().isTrigger = true;

        Debug.Log("here on the patrol state");
    }

    public void UpdateWaypoint(Waypoint wp)
    {
        //Debug.Log("Wp " + wp);
        waypoint = wp;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        float distance = Vector3.Distance(npc.transform.position, target.transform.position);
        if (distance < 5 && npc && target && stateMachine != null)
        {
            stateMachine.ChangeState(npc.chase);
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (waypoint && npc != null)
        {
            npc.transform.position = Vector2.MoveTowards(npc.transform.position, waypoint.transform.position, npc.speed * Time.deltaTime);
            npc.transform.rotation = Quaternion.identity;
        }
    }
}


