using System;
using Unity.VisualScripting;
using UnityEngine;

public class GameSessionController : MonoBehaviour
{
    [SerializeField] protected static GameSessionController instance;
    public static GameSessionController Instance => instance;

    [SerializeField] protected GameState currentState;
    public GameState CurrentState => currentState;

    public event Action<GameState> OnStateChanged;
    private void Awake()
    {
        if (GameSessionController.instance != null)
        {
            Destroy(gameObject);
            return;
        }
        GameSessionController.instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void SetGameState(GameState g)
    {
        if (currentState == g) return;
        this.currentState = g;
        Debug.Log("SetGameState: " + g);

        OnStateChanged?.Invoke(g);
    }
}
