using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatState : BaseState
{
    AIBase aiBase;
    AIStateMachine stateMachine;

    float timer, duration = 0.5f;
    AIBase prey;

    public EatState(AIBase ai, AIStateMachine stateMachine) : base(ai, stateMachine)
    {
        this.aiBase = ai;
        this.stateMachine = stateMachine;
    }

    public override void Enter(BaseState previousState)
    {
        aiBase.ChangeAnimation(EAnimRef.EAT);
        if (aiBase.CheckForPrey())
            prey = aiBase.rangeSensor.ClosestPrey;
    }

    public override void Exit()
    {
    }

    public override void TickLogic()
    {
        if (Utilities.StateBuffer(ref timer, duration))
        {
            aiBase.energy.Modify(50);
            prey.energy.Modify(-50);

            if (prey.energy.CurrentEnergy <= 0)
                prey = null;
        }

        if (prey == null || aiBase.energy.CurrentEnergy >= aiBase.energy.MaxEnergy * 0.8f)
            stateMachine.ChangeState(new IdleState(aiBase, stateMachine));
    }
}
