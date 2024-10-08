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
        health = GetComponent<Health>();
        health.Init(animal.Health);
        energy = GetComponent<Energy>();
        energy.Init(animal.Energy);

        if (animal.AnimalFaction.factionName == "Plant")
        {
            Navigation.agent.enabled = false;
            rangeSensor.enabled = false;
            health.enabled = false;
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

        if (rangeSensor.ClosestPrey != null)
            return true;

        return false;
    }
}
