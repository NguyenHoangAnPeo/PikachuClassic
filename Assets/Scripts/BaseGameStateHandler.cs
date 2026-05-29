using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseGameStateHandler : MonoBehaviour
{
    protected Dictionary<GameState, Action> stateHandlers;
    protected virtual void Awake()
    {
        stateHandlers = new Dictionary<GameState, Action>();
        RegisterState();
    }
    protected virtual void OnEnable()
    {   if (GameSessionController.Instance == null) return;
        GameSessionController.Instance.OnStateChanged += HandleState;
    }
    protected virtual void OnDisable()
    {
        if (GameSessionController.Instance == null) return;
        GameSessionController.Instance.OnStateChanged -= HandleState;
    }
    protected abstract void RegisterState();
    private void HandleState(GameState state)
    {
        if(stateHandlers.TryGetValue(state,out var action))
        {
            action?.Invoke();
        }
    }
}
