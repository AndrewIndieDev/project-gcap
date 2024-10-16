using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AttackState : BaseState
{
    AIBase aiBase;
    AIStateMachine stateMachine;

    float attackTimer, attackTime = 1.3f;

    AIBase target;
    bool isSelfDefence;

    public AttackState(AIBase ai, AIStateMachine stateMachine, AIBase target, bool isSelfDefence) : base(ai, stateMachine)
    {
        this.aiBase = ai;
        this.stateMachine = stateMachine;
        this.target = target;
        this.isSelfDefence = isSelfDefence;
    }

    public override void Enter(BaseState previousState)
    {
        aiBase.ChangeAnimation(EAnimRef.ATTACK);
        if(target)
            Debug.Log($"{aiBase.animal.AnimalName} is currently targeting {target.animal.AnimalName}");
    }

    public override void Exit()
    {
    }

    public override void TickLogic()
    {
        if (!target)
        {
            Debug.Log($"{aiBase.animal.AnimalName} has entered AttackState without a target!");
            return;
        }
            

        FaceTarget(target.transform.position);
        if (Utilities.StateBuffer(ref attackTimer, attackTime))
        {
            target.health.Damage(aiBase.animal.attackDamage, aiBase);

            if (aiBase.energy.CurrentEnergy <= aiBase.energy.MaxEnergy * Mathf.Clamp(aiBase.animal.energyToEat, 0, 1))
            {
                if(target.health.CurrentHealth <= 0)
                {
                    if (aiBase.animal.AnimalFaction.preyFactions.Contains(target.animal.AnimalFaction))
                        stateMachine.ChangeState(new EatState(aiBase, stateMachine));
                    else
                        stateMachine.ChangeState(new WanderState(aiBase, stateMachine));
                    return;
                }
            }

            if (Vector3.Distance(target.transform.position, aiBase.transform.position) > 1.6f)
            {
                stateMachine.ChangeState(new HuntState(aiBase, stateMachine, target, aiBase.animal.isDefensiveOnHit));
                return;
            }
            return;
        }

        /*
        if (!isSelfDefence)
        {
            if (aiBase.CheckForPrey())
                FaceTarget(target.transform.position);
            if (Utilities.StateBuffer(ref attackTimer, attackTime))
            {
                target.health.Damage(aiBase.animal.attackDamage, aiBase);

                if (target.health.CurrentHealth <= 0)
                    stateMachine.ChangeState(new EatState(aiBase, stateMachine));
                if (Vector3.Distance(target.transform.position, aiBase.transform.position) > 1.5f)
                {
                    stateMachine.ChangeState(new HuntState(aiBase, stateMachine, false));
                    return;
                }
                return;
            }
        } else
        {
            if (aiBase.CheckForPredators())
                FaceTarget(target.transform.position);
            if (Utilities.StateBuffer(ref attackTimer, attackTime))
            {
                target.health.Damage(aiBase.animal.attackDamage, aiBase);

                if (target.health.CurrentHealth <= 0)
                    stateMachine.ChangeState(new WanderState(aiBase, stateMachine));
                if (Vector3.Distance(target.transform.position, aiBase.transform.position) > 1.5f)
                {
                    stateMachine.ChangeState(new HuntState(aiBase, stateMachine, true));
                    return;
                }
                return;
            }
        }
        */
    }

    private void FaceTarget(Vector3 destination)
    {
        Vector3 lookPos = destination - aiBase.transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        aiBase.transform.rotation = Quaternion.Slerp(aiBase.transform.rotation, rotation, Time.deltaTime * 5f);
    }
}
