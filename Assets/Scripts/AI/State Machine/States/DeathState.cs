using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : BaseState
{
    AIBase aiBase;
    AIStateMachine stateMachine;

    public DeathState(AIBase ai, AIStateMachine stateMachine) : base(ai, stateMachine)
    {
        this.aiBase = ai;
        this.stateMachine = stateMachine;
    }

    public override void Enter(BaseState previousState)
    {
        aiBase.Navigation?.ResetNavigation();
        aiBase.ChangeAnimation(EAnimRef.DEATH);
    }

    public override void Exit()
    {
    }

    public override void TickLogic()
    {
        aiBase.energy.Modify(-1);

        if (aiBase.energy.CurrentEnergy <= 0)
            aiBase.Destroy();
    }
}
