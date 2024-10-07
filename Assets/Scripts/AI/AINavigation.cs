using UnityEngine;
using UnityEngine.AI;

public class AINavigation : MonoBehaviour
{
    public NavMeshAgent agent;

    Vector3 targetPosition = Vector3.zero;

    public void UpdatePosition(Vector3 position)
    {
        targetPosition = position;
        agent.SetDestination(targetPosition);
    }

    public void ResetNavigationRate()
    {
        targetPosition = transform.position;
    }
}
