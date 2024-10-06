public abstract class BaseState
{
    protected AIBase ai;
    protected BaseStateMachine stateMachine;

    public BaseState(AIBase ai, BaseStateMachine stateMachine)
    {
        this.ai = ai;
        this.stateMachine = stateMachine;
    }

    public abstract void Enter(BaseStateMachine stateMachine, BaseState previousState);
    public abstract void OnTick(BaseStateMachine stateMachine);
    public abstract void Exit(BaseStateMachine stateMachine);
}

