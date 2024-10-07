using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntState : BaseState
{
    AIBase aiBase;
    AIStateMachine stateMachine;

    float fledTimer, fledTime = 3f;

    public HuntState(AIBase ai, AIStateMachine stateMachine) : base(ai, stateMachine)
    {
        this.aiBase = ai;
        this.stateMachine = stateMachine;
    }

    public override void Enter(BaseState previousState)
    {
        aiBase.ChangeAnimation(EAnimRef.RUN);
        aiBase.Navigation.agent.speed *= 1.5f;
    }

    public override void Exit()
    {
        aiBase.Navigation.agent.speed /= 1.5f;
    }

    public override void TickLogic()
    {
        aiBase.CheckRangeSensor();

        if (aiBase.rangeSensor.TargetEntity)
            aiBase.Navigation.UpdatePosition(aiBase.rangeSensor.TargetEntity.transform.position);
        else if (Utilities.Buffer(ref fledTimer, fledTime))
        {
            stateMachine.ChangeState(new WanderState(aiBase, stateMachine));
        }
    }
}
