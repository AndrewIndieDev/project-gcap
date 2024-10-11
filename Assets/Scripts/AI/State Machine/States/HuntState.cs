using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntState : BaseState
{
    AIBase aiBase;
    AIStateMachine stateMachine;

    float fledTimer, fledTime = 3f;

    bool isSelfDefence;
    float selfDefTimer, selfDefTime = 15f;

    public HuntState(AIBase ai, AIStateMachine stateMachine, bool isSelfDefence) : base(ai, stateMachine)
    {
        this.aiBase = ai;
        this.stateMachine = stateMachine;
        this.isSelfDefence = isSelfDefence;
    }

    public override void Enter(BaseState previousState)
    {
        aiBase.ChangeAnimation(EAnimRef.RUN);
        aiBase.Navigation.agent.speed = aiBase.animal.runSpeed;
    }

    public override void Exit()
    {
        aiBase.Navigation.agent.speed = aiBase.animal.Speed;
    }

    public override void TickLogic()
    {
        if(Utilities.StateBuffer(ref selfDefTimer, selfDefTime))
            stateMachine.ChangeState(new WanderState(aiBase, stateMachine));

        if (aiBase.CheckForPredators() && !isSelfDefence)
        {
            stateMachine.ChangeState(new FleeState(aiBase, stateMachine));
            return;
        }

        if (!isSelfDefence)
        {
            if (!aiBase.CheckForPrey())
            {
                stateMachine.ChangeState(new IdleState(aiBase, stateMachine));
                return;
            }

            if (aiBase.rangeSensor.ClosestPrey)
                aiBase.Navigation.UpdatePosition(aiBase.rangeSensor.ClosestPrey.transform.position);
            else if (Utilities.StateBuffer(ref fledTimer, fledTime))
            {
                stateMachine.ChangeState(new WanderState(aiBase, stateMachine));
                return;
            }

            if (Vector3.Distance(aiBase.transform.position, aiBase.rangeSensor.ClosestPrey.transform.position) < 1.5f)
            {
                if (aiBase.rangeSensor.ClosestPrey.animal.AnimalFaction.factionName == "Plant")
                    stateMachine.ChangeState(new EatState(aiBase, stateMachine));
                else
                {
                    if (aiBase.rangeSensor.ClosestPrey.health.CurrentHealth <= 0)
                        stateMachine.ChangeState(new EatState(aiBase, stateMachine));
                    else
                        stateMachine.ChangeState(new AttackState(aiBase, stateMachine, aiBase.rangeSensor.ClosestPrey, isSelfDefence));

                }
                return;
            }
        } else
        {
            if (!aiBase.CheckForPredators())
            {
                stateMachine.ChangeState(new IdleState(aiBase, stateMachine));
                return;
            }

            if (aiBase.rangeSensor.ClosestPredator)
                aiBase.Navigation.UpdatePosition(aiBase.rangeSensor.ClosestPredator.transform.position);
            else if (Utilities.StateBuffer(ref fledTimer, fledTime))
            {
                stateMachine.ChangeState(new WanderState(aiBase, stateMachine));
                return;
            }

            if (Vector3.Distance(aiBase.transform.position, aiBase.rangeSensor.ClosestPredator.transform.position) < 1.5f)
            {
                if (aiBase.rangeSensor.ClosestPredator.health.CurrentHealth <= 0)
                    stateMachine.ChangeState(new WanderState(aiBase, stateMachine));
                else
                    stateMachine.ChangeState(new AttackState(aiBase, stateMachine, aiBase.rangeSensor.ClosestPredator, isSelfDefence));
                return;
            }
        }
        

        
    }
}
