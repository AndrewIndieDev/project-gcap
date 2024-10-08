using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeState : BaseState
{
    AIBase aiBase;
    AIStateMachine stateMachine;

    AIBase predator;
    float fleeTimer, fleeTime = 10f;

    public FleeState(AIBase ai, AIStateMachine stateMachine) : base(ai, stateMachine)
    {
        this.aiBase = ai;
        this.stateMachine = stateMachine;
    }

    public override void Enter(BaseState previousState)
    {
        aiBase.ChangeAnimation(EAnimRef.RUN);
        aiBase.Navigation.agent.speed *= 1.5f;
        predator = aiBase.rangeSensor.ClosestPredator;

        /*
        if (aiBase.CheckForPredators())
            predator = aiBase.rangeSensor.ClosestPredator;
        else
            stateMachine.ChangeState(new WanderState(aiBase, stateMachine));
        */
    }

    public override void Exit()
    {
        aiBase.Navigation.agent.speed /= 1.5f;
    }

    public override void TickLogic()
    {
        if(!predator)
        {
            stateMachine.ChangeState(new WanderState(aiBase, stateMachine));
            return;
        }

        if(Utilities.Buffer(ref fleeTimer, fleeTime))
        {
            if(Vector3.Distance(aiBase.transform.position, predator.transform.position) > 20f)
            {
                stateMachine.ChangeState(new WanderState(aiBase, stateMachine));
                return;
            }
        }

        Vector3 fleeVector = (predator.transform.position - aiBase.transform.position).normalized;
        aiBase.Navigation.UpdatePosition((aiBase.transform.position - fleeVector));
    }
}
