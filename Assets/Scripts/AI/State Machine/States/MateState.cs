using UnityEngine;

public class MateState : BaseState
{
    AIBase aiBase;
    AIStateMachine stateMachine;

    private float mateTimer, mateDuration = 5f;

    public MateState(AIBase ai, AIStateMachine stateMachine) : base(ai, stateMachine)
    {
        this.aiBase = ai;
        this.stateMachine = stateMachine;
    }

    public override void Enter(BaseState previousState)
    {
        aiBase.isMatingWith = aiBase.rangeSensor.ClosestMate;
        aiBase.ChangeAnimation(EAnimRef.WALK);
        aiBase.Navigation.agent.SetDestination(aiBase.isMatingWith.transform.position);
    }

    public override void Exit()
    {
        ResetMating();
    }

    public override void TickLogic()
    {
        if (aiBase.CheckForPredators())
        {
            stateMachine.ChangeState(new FleeState(aiBase, stateMachine));
            return;
        }

        if (aiBase.Navigation.agent.remainingDistance > 0.5f)
            aiBase.Navigation.agent.SetDestination(aiBase.isMatingWith.transform.position);
        else
            aiBase.Navigation.agent.ResetPath();

        if (!aiBase.Navigation.agent.hasPath)
        {
            if (Utilities.StateBuffer(ref mateTimer, mateDuration))
            {
                aiBase.SpawnBaby();
                stateMachine.ChangeState(new IdleState(aiBase, stateMachine));
            }
        }
    }

    private void ResetMating()
    {
        aiBase.isMatingWith = null;
        aiBase.ticksTillMate = aiBase.animal.ticksTillMate;
    }
}
