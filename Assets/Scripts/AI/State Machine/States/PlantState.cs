using UnityEngine;

public class PlantState : BaseState
{
    AIBase aiBase;
    AIStateMachine stateMachine;

    public PlantState(AIBase ai, AIStateMachine stateMachine) : base(ai, stateMachine)
    {
        this.aiBase = ai;
        this.stateMachine = stateMachine;
    }

    public override void Enter(BaseState previousState)
    {
        aiBase.energy.DisableTick();
    }

    public override void Exit()
    {
    }

    public override void TickLogic()
    {
        if (aiBase.energy.CurrentEnergy <= 0)
        {
            stateMachine.ChangeState(new DeathState(aiBase, stateMachine));
            return;
        }

        float scale = aiBase.energy.CurrentEnergy / aiBase.energy.MaxEnergy;
        if (aiBase.visualRoot != null)
            aiBase.visualRoot.localScale = Vector3.one * scale;

        aiBase.energy.Modify(3);

        if (aiBase.energy.CurrentEnergy >= aiBase.energy.MaxEnergy * 0.9f)
        {
            float chance = Random.Range(0f, 1f);
            if (chance <= 0.01f)
            {
                Collider[] hitColliders = Physics.OverlapSphere(aiBase.transform.position, 5f);
                int nearbyPlants = 0;

                foreach (Collider collider in hitColliders)
                {
                    AIBase hit = collider.GetComponent<AIBase>();
                    if (hit != null && hit != aiBase)
                    {
                        if (hit.animal.AnimalFaction.factionName == "Plant")
                        {
                            nearbyPlants++;
                        }
                    }
                }

                if (nearbyPlants < 4)
                {
                    Vector3 position = Vector3.zero;
                    while (position == Vector3.zero)
                        position = GameManager.Instance.GetRandomPositionAroundTarget(aiBase.transform.position, 5f);

                    GameObject go = GameManager.Instantiate(GameManager.Instance.animalPrefab);
                    go.transform.position = GameManager.Instance.GetRandomPositionAroundTarget(aiBase.transform.position, 5f);
                    go.transform.rotation = Quaternion.LookRotation(new Vector3(Random.Range(-20f, 20f), 0f, Random.Range(-20f, 20f)).normalized);
                }
            }
        }
    }
}
