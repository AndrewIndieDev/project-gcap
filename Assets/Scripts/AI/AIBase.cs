using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.UIElements;

public class AIBase : MonoBehaviour
{
    public AnimalSO animal;
    public AINavigation Navigation;
    public Transform visualRoot;
    public Animator anim;
    public AIRangeSensor rangeSensor;
    public Health health;
    public Energy energy;
    public ulong ticksTillMate;
    public AIBase isMatingWith;
    public ulong lifeTick = 1;
    public bool IsAdult => dinoScript.DinoAge >= 10;

    [Header("Feedbacks")]
    public MMF_Player spawnFeedbacks;

    private DinoControll dinoScript;
    private bool isBeingDestroyed;

    public void Start()
    {
        InitializeAnimal();
    }

    public void InitializeAnimal()
    {
        if (!animal)
            return;

        ticksTillMate = animal.ticksTillMate;
        Navigation.agent.speed = animal.Speed;
        GameObject go = Instantiate(animal.AnimalVisual, visualRoot);
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;
        anim = go.GetComponent<Animator>();
        health.Init(animal.Health);

        // Is a Plant
        if (animal.AnimalFaction.factionName == "Plant")
        {
            Navigation.agent.enabled = false;
            rangeSensor.enabled = false;
            health.enabled = false;
            float randomScale = Random.Range(.8f, 1.2f);
            visualRoot.transform.GetChild(0).localScale = Vector3.one * randomScale;

            if (TickSystem.CurrentTick >= 2)
                energy.Init(animal.Energy, 100f);
            else
                energy.Init(animal.Energy);
        }
        // Is a Dino
        else
        {
            spawnFeedbacks.PlayFeedbacks();
            energy.Init(animal.Energy);
            TickSystem.onTick += OnTick;
            dinoScript = visualRoot.GetChild(0).GetComponent<DinoControll>();
        }
        
    }

    private void OnDestroy()
    {
        if (animal.AnimalFaction.factionName != "Plant")
            TickSystem.onTick -= OnTick;
    }

    private void OnTick(int tickIndex)
    {
        if (health.CurrentHealth <= 0 || spawnFeedbacks.IsPlaying)
            return;

        if (ticksTillMate > 0)
            ticksTillMate--;

        lifeTick++;

        if (lifeTick >= animal.ticksTillDeath)
            health.Damage(2f, false);

        float growth = Mathf.Clamp01(lifeTick / (animal.ticksTillDeath * 0.3f));
        if (growth <= 1)
        {
            dinoScript.DinoAge = growth * 10;
            dinoScript.SetGrowth(growth);
        }

        Navigation.agent.speed = growth.Remap(0, 1, animal.babySpeed, animal.Speed);
    }

    public void SetAge(float percent)
    {
        lifeTick = (ulong)(percent * animal.ticksTillDeath);
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

    public bool CheckForMate()
    {
        if (!rangeSensor || !IsAdult)
            return false;

        // Find a mate, and if both are above 50% energy, then mate
        AIBase potentialMate = rangeSensor.ClosestMate;
        if (potentialMate != null && potentialMate.IsAdult && ticksTillMate <= 0 && potentialMate.ticksTillMate <= 0)
        {
            if (isMatingWith == null && potentialMate.isMatingWith == null)
            {
                return true;
            }
            else if (isMatingWith == null && potentialMate.isMatingWith == this)
            {
                return true;
            }
        }

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

    public void SpawnBaby()
    {
        GameObject go = Instantiate(GameManager.Instance.animalPrefab);
        go.transform.position = transform.position;
        go.transform.rotation = transform.rotation;
        go.GetComponent<AIBase>().animal = animal;
    }
}
