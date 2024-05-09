public abstract class BaseState
{
    // Contains an instance of enemy class.
    // Contains an instance of state machine class.

    // Initialize game properties, similar to Start() method.
    public abstract void Enter();
    // Update our state and called every frame our state is active.
    public abstract void Perform();
    // Called on the active state before changing into a new state.
    public abstract void Exit();
}