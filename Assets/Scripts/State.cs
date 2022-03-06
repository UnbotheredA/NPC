using UnityEngine;


public abstract class State 
{
    public NPC npc;

    //REFERENCE TO THE STATE MACHINE
    public StateMachine stateMachine;

    protected State(NPC npc, StateMachine stateMachine)
    {
        this.npc = npc;
        this.stateMachine = stateMachine;
    }
    public virtual void LogicUpdate()
    {

    }
    public virtual void PhysicsUpdate()
    {
        
    }
    public virtual void Enter()
    {
        
    }
    public virtual void Exit()
    {

    }
}
