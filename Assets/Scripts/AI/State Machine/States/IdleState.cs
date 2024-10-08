using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
    AIBase aiBase;
    AIStateMachine stateMachine;

    float idleTimer, idleTime;
    float minTime = 5f, maxTime = 10f;

    public IdleState(AIBase ai, AIStateMachine stateMachine) : base(ai, stateMachine)
    {
        this.aiBase = ai;
        this.stateMachine = stateMachine;
    }

    public override void Enter(BaseState previousState)
    {
        aiBase.ChangeAnimation(EAnimRef.IDLE);
        idleTime = Random.Range(minTime, maxTime) /* / TickSystem.UpdateAnimalsEveryXTicks*/;
        aiBase.Navigation.ResetNavigation();
    }

    public override void Exit()
    {
    }

    public override void TickLogic()
    {
        if (aiBase.energy.CurrentEnergy <= 0)
        {
            stateMachine.ChangeState(new SleepState(aiBase, stateMachine));
        }

        if (aiBase.CheckForPredators())
        {
            stateMachine.ChangeState(new FleeState(aiBase, stateMachine));
            return;
        }

        if (aiBase.CheckForPrey())
        {
            stateMachine.ChangeState(new HuntState(aiBase, stateMachine));
            return;
        }

        if (Utilities.Buffer(ref idleTimer, idleTime))
        {
            stateMachine.ChangeState(new WanderState(aiBase, stateMachine));
            return;
        }
    }
}
