using UnityEngine;

[CreateAssetMenu(menuName = "AI/State Machines/Animal State Machine")]
public class AnimalStateMachine : BaseStateMachine
{
    public override void Initialize(MonoBehaviour owner)
    {
        base.Initialize(owner);
        if (currentState != null)
        {
            currentState.Enter(this, null);
        }
    }

    public override void OnTick()
    {
        if (currentState != null)
        {
            currentState.OnTick(this);
        }
    }

}