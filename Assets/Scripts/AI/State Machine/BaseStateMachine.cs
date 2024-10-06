using UnityEditorInternal;
using UnityEngine;

public abstract class BaseStateMachine : ScriptableObject
{
    protected MonoBehaviour owner;  // The MonoBehaviour that owns this state machine
    protected BaseState currentState;

    // Method to initialize the state machine with the owner reference
    public virtual void Initialize(MonoBehaviour owner)
    {
        this.owner = owner;
    }

    // Method to update the state machine each frame
    public abstract void OnTick();

    // Method to change the state
    public virtual void ChangeState(BaseState newState)
    {
        Debug.Log($"[AI] Changing state from {currentState} to {newState}.");

        if (currentState != null)
            currentState.Exit(this);
        //aiBase.Navigation.ResetNavigationRate();
        BaseState oldState = currentState;

        currentState = newState;
        currentState.Enter(this, oldState);
    }
}

