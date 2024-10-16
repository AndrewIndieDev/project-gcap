using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeState : BaseState
{
    AIBase aiBase;
    AIStateMachine stateMachine;

    AIBase target;
    float fleeTimer, fleeTime = 10f;

    public FleeState(AIBase ai, AIStateMachine stateMachine) : base(ai, stateMachine)
    {
        this.aiBase = ai;
        this.stateMachine = stateMachine;
    }

    public override void Enter(BaseState previousState)
    {
        aiBase.ChangeAnimation(EAnimRef.RUN);
        aiBase.Navigation.agent.speed = aiBase.animal.runSpeed;

        if(!aiBase.health.LastAttacker)
        {
            if (aiBase.CheckForPredators())
                target = aiBase.rangeSensor.ClosestPredator;
            else
                stateMachine.ChangeState(new WanderState(aiBase, stateMachine));
        }
        else
            target = aiBase.health.LastAttacker;

        /*
        if (aiBase.CheckForPredators())
            target = aiBase.rangeSensor.ClosestPredator;
        else if (aiBase.animal.isDefensiveOnHit && aiBase.CheckForPrey())
            target = aiBase.rangeSensor.ClosestPrey;
        else
            stateMachine.ChangeState(new WanderState(aiBase, stateMachine));
        */
    }

    public override void Exit()
    {
        aiBase.Navigation.agent.speed = aiBase.animal.Speed;
    }

    public override void TickLogic()
    {
        if(!target)
        {
            stateMachine.ChangeState(new WanderState(aiBase, stateMachine));
            return;
        }

        if(Utilities.StateBuffer(ref fleeTimer, fleeTime))
        {
            if(Vector3.Distance(aiBase.transform.position, target.transform.position) > 20f)
            {
                stateMachine.ChangeState(new WanderState(aiBase, stateMachine));
                return;
            }
        }

        Vector3 fleeVector = (target.transform.position - aiBase.transform.position).normalized;
        aiBase.Navigation.UpdatePosition((aiBase.transform.position - fleeVector));
    }
}
