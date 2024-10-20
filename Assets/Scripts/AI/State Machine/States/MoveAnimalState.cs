using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAnimalState : BaseState
{
    AIBase aiBase;
    AIStateMachine stateMachine;
    
    public MoveAnimalState(AIBase ai, AIStateMachine stateMachine) : base(ai, stateMachine)
    {
        this.aiBase = ai;
        this.stateMachine = stateMachine;
    }

    public override void Enter(BaseState previousState)
    {
    }

    public override void Exit()
    {
    }

    public override void TickLogic()
    {
        aiBase.Navigation.UpdatePosition(Vector3.forward * 10);
    }
}
