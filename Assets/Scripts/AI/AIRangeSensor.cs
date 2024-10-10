using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIRangeSensor : MonoBehaviour
{
    private List<AIBase> nearbyPrey = new List<AIBase>();
    public List<AIBase> NearbyEntities {  get { return nearbyPrey; } }
    private List<AIBase> nearbyPredator = new List<AIBase>();
    public List<AIBase> NearbyPredator { get { return nearbyPredator; } }

    [SerializeField] float detectionRange = 3f;

    float commitmentTimer, commitmentTime = 5f;

    public AIBase ClosestPrey;
    private AIBase storedTargetPrey;

    public AIBase ClosestPredator;
    private AIBase storedTargetPredator;

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

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRange);

        foreach (Collider collider in hitColliders)
        {
            if(collider.GetComponent<AIBase>() != null && collider.GetComponent<AIBase>() != GetComponent<AIBase>())
            {
                AIBase ai = collider.GetComponent<AIBase>();
                if (aiBase.animal.AnimalFaction.preyFactions.Contains(ai.animal.AnimalFaction))
                    nearbyPrey.Add(collider.GetComponent<AIBase>());

                if (aiBase.animal.AnimalFaction.predatorFactions.Contains(ai.animal.AnimalFaction))
                    nearbyPredator.Add(collider.GetComponent<AIBase>());
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

            if (!ClosestPrey)
                storedTargetPrey = null;

            if (!ClosestPredator)
                storedTargetPredator = null;
        }
        else
        {
            if (storedTargetPrey != null)
                ClosestPrey = storedTargetPrey;
            if (storedTargetPredator != null)
                ClosestPredator = storedTargetPredator;
        }
    }
}
