using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
    AIBase aiBase;
    AIStateMachine stateMachine;

    float idleTimer, idleTime;
    float minTime = 5f, maxTime = 10f;

    float spawnDelayTime, spawnDelayDuration = 2f;
    bool isSpawning = true;

    public IdleState(AIBase ai, AIStateMachine stateMachine) : base(ai, stateMachine)
    {
        this.aiBase = ai;
        this.stateMachine = stateMachine;
    }

    public override void Enter(BaseState previousState)
    {
        aiBase.ChangeAnimation(EAnimRef.IDLE);
        if (previousState != null)
            idleTime = Random.Range(minTime, maxTime);
        else
            idleTime = 0.1f;
        aiBase.Navigation.ResetNavigation();
    }

    public override void Exit()
    {
    }

    public override void TickLogic()
    {
        if (Utilities.StateBuffer(ref spawnDelayTime, spawnDelayDuration))
        {
            isSpawning = false;
        }

        if (isSpawning)
            return;

        if (aiBase.animal.AnimalFaction.factionName == "Plant")
        {
            stateMachine.ChangeState(new PlantState(aiBase, stateMachine));
            return;
        }

        if (aiBase.energy.CurrentEnergy <= 0)
        {
            stateMachine.ChangeState(new SleepState(aiBase, stateMachine));
            return;
        }

        if (aiBase.CheckForPredators())
        {
            stateMachine.ChangeState(new FleeState(aiBase, stateMachine));
            return;
        }

        if (aiBase.CheckForPrey())
        {
            stateMachine.ChangeState(new HuntState(aiBase, stateMachine, false));
            return;
        }

        if (Utilities.StateBuffer(ref idleTimer, idleTime))
        {
            stateMachine.ChangeState(new WanderState(aiBase, stateMachine));
            return;
        }
    }
}
