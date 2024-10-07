public abstract class BaseState
{
    protected AIBase ai;
    protected AIStateMachine stateMachine;

    public BaseState(AIBase ai, AIStateMachine stateMachine)
    {
        this.ai = ai;
        this.stateMachine = stateMachine;
    }

    public abstract void Enter(BaseState previousState);
    public abstract void TickLogic();
    public abstract void Exit();
}
