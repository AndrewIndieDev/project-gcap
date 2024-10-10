using MoreMountains.Feedbacks;
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

    [Header("Feedbacks")]
    public MMF_Player spawnFeedbacks;


    private bool isBeingDestroyed;

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
        energy.Init(animal.Energy);

        float randomScale;
        if (animal.AnimalFaction.factionName == "Plant")
        {
            Navigation.agent.enabled = false;
            rangeSensor.enabled = false;
            health.enabled = false;
            randomScale = Random.Range(.8f, 1.2f);
        }
        else
        {
            spawnFeedbacks.PlayFeedbacks();
            randomScale = Random.Range(.9f, 1.1f);
        }
        visualRoot.transform.GetChild(0).localScale = Vector3.one * randomScale;
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

        if (rangeSensor.ClosestPrey != null && energy.CurrentEnergy <= energy.MaxEnergy * .6f)
            return true;

        return false;
    }

    public void Destroy()
    {
        if (isBeingDestroyed)
            return;

        isBeingDestroyed = true;
        Destroy(visualRoot.gameObject);
        Invoke(nameof(DelayedDestroy), 2f);
    }

    private void DelayedDestroy()
    {
        Destroy(gameObject);
    }
}
