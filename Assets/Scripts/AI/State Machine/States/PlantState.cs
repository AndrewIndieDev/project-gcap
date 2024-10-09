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

        aiBase.energy.Modify(1);

        if (aiBase.energy.CurrentEnergy == aiBase.energy.MaxEnergy)
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

                if (nearbyPlants < 10)
                {
                    GameObject go = GameManager.Instantiate(GameManager.Instance.animalPrefab);
                    go.transform.position = GetRandomPositionAroundTarget(aiBase.transform.position, 5f);
                    go.transform.rotation = Quaternion.LookRotation(new Vector3(Random.Range(-20f, 20f), 0f, Random.Range(-20f, 20f)).normalized);
                    go.GetComponent<AIBase>().energy.SetCurrentEnergy(1);
                }
            }
        }
    }

    Vector3 GetRandomPositionAroundTarget(Vector3 targetPosition, float radius)
    {
        Vector2 randomCircle = Random.insideUnitCircle * radius; // Get a random point in a circle
        Vector3 randomPosition = new Vector3(randomCircle.x, 0, randomCircle.y) + targetPosition; // Adjust for 3D space
        return randomPosition;
    }
}
