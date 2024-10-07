using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRangeSensor : MonoBehaviour
{
    private List<AIBase> nearbyEntities = new List<AIBase>();
    public List<AIBase> NearbyEntities {  get { return nearbyEntities; } }

    [SerializeField] float detectionRange = 3f;

    // Start is called before the first frame update
    void Start()
    {
        TickSystem.onTick += OnTick;
    }

    // Update is called once per tick
    void OnTick()
    {
        nearbyEntities.Clear();
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRange);
    }
}
