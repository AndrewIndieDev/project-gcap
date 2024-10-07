using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TickSystem;

[RequireComponent(typeof(AIBase))]
public class AIStateMachine : MonoBehaviour
{
    public BaseState currentState;

    //States
    public MoveAnimalState moveState;

    AIBase aiBase;
    
    // Start is called before the first frame update
    void Start()
    {
        TickSystem.onTick += OnTick;
        aiBase = GetComponent<AIBase>();
        moveState = new MoveAnimalState(aiBase, this);
        currentState = moveState;
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
