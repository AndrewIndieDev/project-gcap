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
            if(aiBase.animal.AnimalFaction.factionName == "Plant")
                currentState = new PlantState(aiBase, this);
            else
                currentState = new IdleState(aiBase, this);
        }
    }

    void OnTick()
    {
        if (currentState != null)
            currentState.TickLogic();
    }

    public void ChangeState(BaseState newState)
    {
        Debug.Log($"[AI] Changing state from {currentState} to {newState}.");

        if (currentState != null)
            currentState.Exit();
        aiBase.Navigation.ResetNavigationRate();
        BaseState oldState = currentState;

        currentState = newState;
        currentState.Enter(oldState);
    }
}
