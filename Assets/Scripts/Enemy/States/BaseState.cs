public abstract class BaseState
{
    private Enemy enemy; 
    private StateMachine stateMachine;

    // Initialize game properties, similar to Start() method.
    public abstract void Enter();
    // Update our state and called every frame our state is active.
    public abstract void Perform();
    // Called on the active state before changing into a new state.
    public abstract void Exit();

    public void SetStateMachine(StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
}