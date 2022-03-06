public class StateMachine
{
    // this class will will keep track of the current state of the state machine
    public State CurrentState { get; private set; }
    public void Initialize(State startingState)
    {
        CurrentState = startingState;
        startingState.Enter();
        //Debug.Log("The starting state: " + startingState.ToString());
    }

    public void ChangeState(State newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        //Debug.Log("new state happend " + newState);
        newState.Enter();
    }
}
