using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TickSystem;

[RequireComponent(typeof(AIBase))]
public class AIStateMachine : MonoBehaviour
{
    public BaseState currentState;
    AIBase aiBase;
    
    // Start is called before the first frame update
    void Start()
    {
        TickSystem.onTick += OnTick;
        aiBase = GetComponent<AIBase>();

        if (aiBase.animal)
        {
            if (aiBase.animal.AnimalFaction.factionName == "Plant")
                ChangeState(new PlantState(aiBase, this));
            else
                ChangeState(new WanderState(aiBase, this));
        }
    }

    void OnTick()
    {
        if (currentState != null)
            currentState.TickLogic();
    }

    public void ChangeState(BaseState newState)
    {
        Debug.Log($"[AI] Changing state from {(currentState != null ? currentState : "<null>")} to {(newState != null ? newState : "<null>")}.");

        if (currentState != null)
            currentState.Exit();
        aiBase.Navigation.ResetNavigation();
        BaseState oldState = currentState;

        currentState = newState;
        currentState.Enter(oldState);
    }
}
