public class SleepState : BaseState
{
    AIBase aiBase;
    AIStateMachine stateMachine;

    float wakeTimer, wakeDuration = 2f;

    public bool getUpImmediately;

    public SleepState(AIBase ai, AIStateMachine stateMachine) : base(ai, stateMachine)
    {
        this.aiBase = ai;
        this.stateMachine = stateMachine;
    }

    public override void Enter(BaseState previousState)
    {
        aiBase.ChangeAnimation(EAnimRef.SLEEP);
        aiBase.Navigation.ResetNavigation();
        aiBase.energy.DisableTick();
    }

    public override void Exit()
    {
        aiBase.energy.EnableTick();
    }

    public override void TickLogic()
    {
        aiBase.energy.Modify(10);
        float damage = 10f;
        aiBase.health.Damage(damage, false);

        if (aiBase.energy.CurrentEnergy >= aiBase.energy.MaxEnergy * 0.5f || getUpImmediately)
        {
            aiBase.ChangeAnimation(EAnimRef.WAKE_UP);
            if (Utilities.StateBuffer(ref wakeTimer, wakeDuration))
            {
                stateMachine.ChangeState(new IdleState(aiBase, stateMachine));
            }
        }
    }
}
