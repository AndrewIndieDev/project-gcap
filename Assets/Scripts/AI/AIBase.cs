using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBase : MonoBehaviour
{
    [SerializeField] BaseStateMachine stateMachine;
    
    private void Start()
    {
        TickSystem.onTick += OnTick;
        if (stateMachine != null)
            stateMachine.Initialize(this);
    }

    private void OnTick()
    {
        if (stateMachine != null)
            stateMachine.OnTick();
    }
}
