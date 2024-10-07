using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRangeSensor : MonoBehaviour
{
    private List<AIBase> nearbyEntities = new List<AIBase>();
    public List<AIBase> NearbyEntities {  get { return nearbyEntities; } }

    [SerializeField] float detectionRange = 3f;

    float commitmentTimer, commitmentTime = 5f;

    public AIBase TargetEntity;

    // Start is called before the first frame update
    void Start()
    {
        TickSystem.onTick += OnTick;
        commitmentTimer = commitmentTime;
    }

    // Update is called once per tick
    void OnTick()
    {
        nearbyEntities.Clear();
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRange);

        foreach (Collider collider in hitColliders)
        {
            if(collider.GetComponent<AIBase>() != null && collider.GetComponent<AIBase>() != GetComponent<AIBase>())
                nearbyEntities.Add(collider.GetComponent<AIBase>());
        }

        if(Utilities.Buffer(ref commitmentTime, commitmentTime))
        {
            float closestDistance = 99;
            for(int i = 0; i < nearbyEntities.Count; i++)
            {
                float dist = Vector3.Distance(transform.position, nearbyEntities[i].transform.position);
                if(dist < closestDistance)
                {
                    closestDistance = dist;
                    TargetEntity = nearbyEntities[i];
                }
                    
            }
        }
    }
}
