using UnityEngine;

public class AIBase : MonoBehaviour
{
    public AnimalSO animal;
    public AINavigation Navigation;
    public Transform visualRoot;
    public Animator anim;
    public AIRangeSensor rangeSensor;
    public Health health;
    public Energy energy;

    public void Start()
    {
        InitializeAnimal();
    }

    public void InitializeAnimal()
    {
        if (!animal)
            return;

        Navigation.agent.speed = animal.Speed;
        GameObject go = Instantiate(animal.AnimalVisual, visualRoot);
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;
        anim = go.GetComponent<Animator>();
        health.Init(animal.Health);

        if (animal.AnimalFaction.factionName == "Plant")
        {
            Navigation.agent.enabled = false;
            rangeSensor.enabled = false;
            health.enabled = false;
        }
        else
        {
            energy.Init(animal.Energy);
        }
    }

    public void ChangeAnimation(EAnimRef animation)
    {
        if (anim == null || anim.GetInteger("State") == (int)animation)
            return;

        anim.SetInteger("State", (int)animation);
        anim.SetBool("Reset", true);
    }

    public bool CheckForPredators()
    {
        if (!rangeSensor)
            return false;

        if(rangeSensor.ClosestPredator != null)
            return true;
        
        return false;
    }

    public bool CheckForPrey()
    {
        if (!rangeSensor)
            return false;

        if (rangeSensor.ClosestPrey != null && energy.CurrentEnergy <= energy.MaxEnergy * .25f)
            return true;

        return false;
    }

    public void Destroy()
    {
        Destroy(visualRoot);
        Invoke(nameof(DelayedDestroy), 2f);
    }

    private void DelayedDestroy()
    {
        Destroy(gameObject);
    }
}
