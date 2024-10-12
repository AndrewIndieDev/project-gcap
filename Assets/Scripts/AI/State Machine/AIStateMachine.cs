using UnityEngine;

[RequireComponent(typeof(AIBase))]
public class AIStateMachine : MonoBehaviour
{
    public string CurrentState;
    [Space]
    public BaseState currentState;
    AIBase aiBase;
    int tickIndex = -1;
    
    // Start is called before the first frame update
    void Start()
    {
        TickSystem.onTick += OnTick;
        tickIndex = TickSystem.CurrentTickIndex;
        aiBase = GetComponent<AIBase>();

        if (aiBase.animal)
        {
            if (aiBase.animal.AnimalFaction.factionName == "Plant")
                ChangeState(new PlantState(aiBase, this));
            else
                ChangeState(new IdleState(aiBase, this));
        }

        currentState.TickLogic();
    }

    private void OnDestroy()
    {
        TickSystem.onTick -= OnTick;
    }

    void OnTick(int tickIndex)
    {
        if (currentState != null && tickIndex == this.tickIndex)
        {
            currentState.TickLogic();
            CurrentState = currentState.ToString();
        }

        if (!(currentState is DeathState) && aiBase.animal.ScorePerTick != 0)
        {
            PointSystem.AddPoints(aiBase.animal.ScorePerTick);
        }
    }

    public void ChangeState(BaseState newState)
    {
        //Debug.Log($"[AI] Changing state from {(currentState != null ? currentState : "<null>")} to {(newState != null ? newState : "<null>")}.");

        if (currentState != null)
            currentState.Exit();
        aiBase.Navigation.ResetNavigation();
        BaseState oldState = currentState;

        currentState = newState;
        currentState.Enter(oldState);
    }

    public void OverrideStateToDeath()
    {
        ChangeState(new DeathState(aiBase, this));
    }

    public void OverrideStateToFlee()
    {
        if (CurrentState == "SleepState")
            (currentState as SleepState).getUpImmediately = true;
        else
            ChangeState(new FleeState(aiBase, this));
    }

    public void OverrideOnHit()
    {
        if (CurrentState == "SleepState")
            (currentState as SleepState).getUpImmediately = true;
        else
        {
            if (aiBase.animal.isDefensiveOnHit)
                ChangeState(new HuntState(aiBase, this, true));
            else
                ChangeState(new FleeState(aiBase, this));
        }
    }
}
