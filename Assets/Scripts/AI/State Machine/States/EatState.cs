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
        if (Utilities.Buffer(ref timer, duration))
        {
            aiBase.energy.Modify(100);
            prey.energy.Modify(-100);
        }

        if (prey == null || aiBase.energy.CurrentEnergy >= aiBase.energy.MaxEnergy * 0.8f)
            stateMachine.ChangeState(new IdleState(aiBase, stateMachine));
    }
}
