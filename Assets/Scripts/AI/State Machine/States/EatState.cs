using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatState : BaseState
{
    AIBase aiBase;
    AIStateMachine stateMachine;

    public EatState(AIBase ai, AIStateMachine stateMachine) : base(ai, stateMachine)
    {
        this.aiBase = ai;
        this.stateMachine = stateMachine;
    }

    public override void Enter(BaseState previousState)
    {
        aiBase.ChangeAnimation(EAnimRef.EAT);
    }

    public override void Exit()
    {
    }

    public override void TickLogic()
    {

    }
}
