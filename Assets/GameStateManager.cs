using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : AnMonoBehaviour
{
    [SerializeField] protected static GameStateManager instance;
    public static GameStateManager Instance => instance;
    [SerializeField] protected GameState currentState;
    public GameState CurrentState => currentState;
    [SerializeField] protected List<IGameStateListener> listeners = new List<IGameStateListener>();
    protected override void Awake()
    {
        if (GameStateManager.instance != null)
        {
            Destroy(gameObject);
            return;
        }
        GameStateManager.instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void RegisterListener(IGameStateListener listener)
    {
        if (GameStateManager.instance == null) return;
        if (!listeners.Contains(listener))
        {
            listeners.Add(listener);
        }
    }
    public void UnregisterListener(IGameStateListener listener)
    {

        if (GameStateManager.instance == null) return;
        if (listeners.Contains(listener))
        {
            listeners.Remove(listener);
        }
    }
    public void ChangeState(GameState newState)
    {
        currentState = newState;
        Debug.Log("New State = " + currentState);

        foreach (var listener in listeners)
        {
            listener.OnGameStateChanged(newState);
        }
    }
}
