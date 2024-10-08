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
        if(aiBase.CheckForPredators())
        {
            stateMachine.ChangeState(new FleeState(aiBase, stateMachine));
            return;
        }
        
        if (!aiBase.CheckForPrey())
            return;

        if (aiBase.rangeSensor.ClosestPrey)
            aiBase.Navigation.UpdatePosition(aiBase.rangeSensor.ClosestPrey.transform.position);
        else if (Utilities.Buffer(ref fledTimer, fledTime))
        {
            stateMachine.ChangeState(new WanderState(aiBase, stateMachine));
            return;
        }

        if(Vector3.Distance(aiBase.transform.position, aiBase.rangeSensor.ClosestPrey.transform.position) < 1.5f)
        {
            if(aiBase.rangeSensor.ClosestPrey.animal.AnimalFaction.factionName == "Plant")
                stateMachine.ChangeState(new EatState(aiBase, stateMachine));
            else
            {
                if(aiBase.rangeSensor.ClosestPrey.health.CurrentHealth <= 0)
                    stateMachine.ChangeState(new EatState(aiBase, stateMachine));
                else
                    stateMachine.ChangeState(new AttackState(aiBase, stateMachine, aiBase.rangeSensor.ClosestPrey));

            }
            return;
        }
    }
}
