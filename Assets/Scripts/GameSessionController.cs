using System;
using Unity.VisualScripting;
using UnityEngine;

public class GameSessionController : MonoBehaviour
{
    [SerializeField] protected static GameSessionController instance;
    public static GameSessionController Instance => instance;

    [SerializeField] protected int remainingPokemon;
    public int RemainingPokemon => remainingPokemon;

    [SerializeField] protected int score;
    public int Score => score;

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
    }
    private void Start()
    {
        this.SetGameState(GameState.Playing);
    }
    public virtual void SetRemainingPokemon(int i)
    {
        this.remainingPokemon = i;
    }
    public virtual void SetScore(int score)
    {
        this.score = score;
    }
    public bool IsWin()
    {
        return remainingPokemon == 0;
    }
    public void SetGameState(GameState g)
    {
        if (currentState == g) return;
        this.currentState = g;

        OnStateChanged?.Invoke(g);
    }
}
