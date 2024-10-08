using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    AIBase aiBase;
    AIStateMachine stateMachine;

    float attackTimer, attackTime = 1.3f;

    AIBase target;

    public AttackState(AIBase ai, AIStateMachine stateMachine, AIBase target) : base(ai, stateMachine)
    {
        this.aiBase = ai;
        this.stateMachine = stateMachine;
        this.target = target;
    }

    public override void Enter(BaseState previousState)
    {
        aiBase.ChangeAnimation(EAnimRef.ATTACK);
    }

    public override void Exit()
    {
    }

    public override void TickLogic()
    {
        if(aiBase.CheckForPrey())
            FaceTarget(aiBase.rangeSensor.ClosestPrey.transform.position);
        if (Utilities.Buffer(ref attackTimer, attackTime))
        {
            target.health.Damage(aiBase.animal.attackDamage);

            if(target.health.CurrentHealth <= 0)
                stateMachine.ChangeState(new EatState(aiBase, stateMachine));
            if (Vector3.Distance(aiBase.rangeSensor.ClosestPrey.transform.position, aiBase.transform.position) > 1.5f)
            {
                stateMachine.ChangeState(new HuntState(aiBase, stateMachine));
                return;
            }
            return;
        }

    }

    private void FaceTarget(Vector3 destination)
    {
        Vector3 lookPos = destination - aiBase.transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        aiBase.transform.rotation = Quaternion.Slerp(aiBase.transform.rotation, rotation, Time.deltaTime * 5f);
    }
}
