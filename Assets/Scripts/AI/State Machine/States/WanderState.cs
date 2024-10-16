using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderState : BaseState
{
    AIBase aiBase;
    AIStateMachine stateMachine;

    float wanderPointTimer, wanderPointTime = 5f;

    float wanderTimer, wanderTime = 10f;
    float wanderMin = 5f, wanderMax = 20f;

    public WanderState(AIBase ai, AIStateMachine stateMachine) : base(ai, stateMachine)
    {
        this.aiBase = ai;
        this.stateMachine = stateMachine;
    }

    public override void Enter(BaseState previousState)
    {
        aiBase.ChangeAnimation(EAnimRef.WALK);
        wanderTime = Random.Range(wanderMin, wanderMax) /* / TickSystem.UpdateAnimalsEveryXTicks*/;
        wanderPointTimer = wanderPointTime;

        //Reset last attacker if you reach this state again.
        //aiBase.health.LastAttacker = null;
    }

    public override void Exit()
    {
    }

    public override void TickLogic()
    {
        if (aiBase.CheckForPredators())
        {
            if(aiBase.rangeSensor.ClosestPredator.health.CurrentHealth > 0)
            {
                stateMachine.ChangeState(new FleeState(aiBase, stateMachine));
                return;
            }
        }

        if (aiBase.CheckForPrey())
        {
            stateMachine.ChangeState(new HuntState(aiBase, stateMachine, aiBase.rangeSensor.ClosestPrey, false));
            return;
        }

        if (aiBase.CheckForMate())
        {
            stateMachine.ChangeState(new MateState(aiBase, stateMachine));
            return;
        }


        if (Utilities.StateBuffer(ref wanderTimer, wanderTime))
        {
            stateMachine.ChangeState(new IdleState(aiBase, stateMachine));
            return;
        }
            
        
        if(Utilities.StateBuffer(ref wanderPointTimer, wanderPointTime))
        {
            FindWanderPoint();
        }

        // Check if we've reached the destination
        if (!aiBase.Navigation.agent.pathPending)
        {
            if (aiBase.Navigation.agent.remainingDistance <= aiBase.Navigation.agent.stoppingDistance)
            {
                if (!aiBase.Navigation.agent.hasPath || aiBase.Navigation.agent.velocity.sqrMagnitude == 0f)
                {
                    stateMachine.ChangeState(new IdleState(aiBase, stateMachine));
                    return;
                }
            }
        }
    }

    void FindWanderPoint()
    {
        aiBase.Navigation.UpdatePosition(AINavigation.RandomNavSphere(aiBase.transform.position, 50f, Physics.AllLayers));
    }
}
