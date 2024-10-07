using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    AIBase aiBase;
    AIStateMachine stateMachine;

    float attackTimer, attackTime = 1.3f;

    public AttackState(AIBase ai, AIStateMachine stateMachine) : base(ai, stateMachine)
    {
        this.aiBase = ai;
        this.stateMachine = stateMachine;
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
        if(aiBase.CheckRangeSensor())
            FaceTarget(aiBase.rangeSensor.TargetEntity.transform.position);
        if (Utilities.Buffer(ref attackTimer, attackTime))
        {
            if (Vector3.Distance(aiBase.rangeSensor.TargetEntity.transform.position, aiBase.transform.position) > 1.5f)
                stateMachine.ChangeState(new HuntState(aiBase, stateMachine));

            if (aiBase.CheckRangeSensor())
            {
                aiBase.rangeSensor.TargetEntity.health.Damage(aiBase.animal.attackDamage);
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
