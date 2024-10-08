using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepState : BaseState
{
    AIBase aiBase;
    AIStateMachine stateMachine;

    float wakeTimer, wakeDuration = 2f;

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

        if (aiBase.energy.CurrentEnergy >= aiBase.energy.MaxEnergy * 0.5f)
        {
            aiBase.ChangeAnimation(EAnimRef.WAKE_UP);
            if (Utilities.Buffer(ref wakeTimer, wakeDuration))
            {
                stateMachine.ChangeState(new IdleState(aiBase, stateMachine));
            }
        }
    }
}
