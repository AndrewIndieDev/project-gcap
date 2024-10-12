using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class AIRangeSensor : MonoBehaviour
{
    private List<AIBase> nearbyPrey = new List<AIBase>();
    public List<AIBase> NearbyPrey {  get { return nearbyPrey; } }
    private List<AIBase> nearbyPredator = new List<AIBase>();
    public List<AIBase> NearbyPredator { get { return nearbyPredator; } }
    private List<AIBase> nearbyMates = new List<AIBase>();
    public List<AIBase> NearbyMates { get { return nearbyMates; } }

    [SerializeField] float detectionRange = 3f;

    float commitmentTimer, commitmentTime = 5f;

    public AIBase ClosestPrey;
    private AIBase storedTargetPrey;

    public AIBase ClosestPredator;
    private AIBase storedTargetPredator;

    public AIBase ClosestMate;
    private AIBase storedTargetMate;

    AIBase aiBase;

    // Start is called before the first frame update
    void Start()
    {
        TickSystem.onTick += OnTick;
        commitmentTimer = commitmentTime;

        aiBase = GetComponent<AIBase>();
    }

    private void OnDestroy()
    {
        TickSystem.onTick -= OnTick;
    }

    // Update is called once per tick
    void OnTick(int tickIndex)
    {
        nearbyPrey.Clear();
        nearbyPredator.Clear();
        storedTargetPrey = ClosestPrey;
        storedTargetPredator = ClosestPredator;
        ClosestPrey = null;
        ClosestPredator = null;
        ClosestMate = null;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRange);

        AIBase ai = null;
        foreach (Collider collider in hitColliders)
        {
            ai = collider.GetComponent<AIBase>();
            if (ai != null && ai != aiBase)
            {
                // Find animals of the same species nearby
                if (ai.animal.AnimalName == aiBase.animal.AnimalName)
                {
                    Vector3 dist = transform.position - ai.transform.position;
                    if (storedTargetMate == null || Vector3.Distance(transform.position, storedTargetMate.transform.position) > Vector3.Distance(transform.position, ai.transform.position))
                    {
                        ClosestMate = ai;
                    }
                }

                //// Eat god damn anything to survive
                if (aiBase.animal.favouriteFood.Length <= 0 || aiBase.energy.CurrentEnergy <= aiBase.energy.MaxEnergy * .3f)
                {
                    if (aiBase.animal.AnimalFaction.preyFactions.Contains(ai.animal.AnimalFaction))
                        nearbyPrey.Add(ai);
                } else //Otherwise eat favourite food
                {
                    if (aiBase.animal.favouriteFood.Contains(ai.animal))
                        nearbyPrey.Add(ai);        
                }
                if (aiBase.animal.AnimalFaction.predatorFactions.Contains(ai.animal.AnimalFaction))
                    nearbyPredator.Add(ai);
            }
                
        }

        if (Utilities.Buffer(ref commitmentTime, commitmentTime))
        {
            float closestPreyDistance = 99;
            for (int i = 0; i < nearbyPrey.Count; i++)
            {
                float dist = Vector3.Distance(transform.position, nearbyPrey[i].transform.position);
                if (dist < closestPreyDistance && nearbyPrey[i].energy.CurrentEnergy > 0)
                {
                    closestPreyDistance = dist;
                    ClosestPrey = nearbyPrey[i];
                }
            }

            float closestPredatorDistance = 99;
            for (int i = 0; i < nearbyPredator.Count; i++)
            {
                float dist = Vector3.Distance(transform.position, nearbyPredator[i].transform.position);
                if (dist < closestPredatorDistance)
                {
                    closestPredatorDistance = dist;
                    ClosestPredator = nearbyPredator[i];
                }
            }

            float closestMateDistance = 99;
            for (int i = 0; i < nearbyMates.Count; i++)
            {
                float dist = Vector3.Distance(transform.position, nearbyMates[i].transform.position);
                if (dist < closestMateDistance)
                {
                    closestMateDistance = dist;
                    ClosestMate = nearbyMates[i];
                }
            }

            if (!ClosestPrey)
                storedTargetPrey = null;

            if (!ClosestPredator)
                storedTargetPredator = null;

            if (!ClosestMate)
                storedTargetMate = null;
        }
        else
        {
            if (storedTargetPrey != null)
                ClosestPrey = storedTargetPrey;
            if (storedTargetPredator != null)
                ClosestPredator = storedTargetPredator;
            if (storedTargetMate != null)
                ClosestMate = storedTargetMate;
        }
    }
}
