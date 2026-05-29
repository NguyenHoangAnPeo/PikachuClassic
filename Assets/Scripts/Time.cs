using System;
using UnityEngine;

public class Time : BaseGameStateHandler
{
    [SerializeField] protected float timeLeft = 60f;
    public float TimeLeft => timeLeft;

    protected float timeMax;

    public event Action<float> OnTimeChanged;

    [SerializeField] protected bool isPlaying;
    private void Start()
    {
        if(GameSessionController.Instance == null)return;

        GameSessionController.Instance.SetGameState(GameState.Playing);
    }

    protected override void Awake()
    {
        base.Awake();

        this.SetDataLevel();
    }
    private void Update()
    {
        this.RunTimeCoolDown();
    }
    public virtual void SetTimeLeft(float p)
    {
        this.timeMax = p;
        this.timeLeft = timeMax;
    }
    public bool IsLose()
    {
        return timeLeft <= 0;
    }
    protected void RunTimeCoolDown()
    {
        if (!isPlaying) return;
        if (timeLeft > 0)
        {
            timeLeft = Mathf.Max(0f, timeLeft - UnityEngine.Time.deltaTime);

            OnTimeChanged?.Invoke(timeLeft);
        }
        else
        {
            GameSessionController.Instance.SetGameState(GameState.Lose);
        }
    }

    protected override void RegisterState()
    {
        Debug.Log("RegisterState CALLED");
        stateHandlers.Add(GameState.Start, StartTimer);
        stateHandlers.Add(GameState.Playing, ResumeTimer);
        stateHandlers.Add(GameState.Paused, PauseTimer);
        stateHandlers.Add(GameState.Lose, PauseTimer);
    }
    protected virtual void SetDataLevel()
    {
        var currentLevel = GameManager.Instance.CurrentLevel;

        this.SetTimeLeft(currentLevel.timeLimit);
    }
    private void StartTimer()
    {
        this.timeLeft = timeMax;
        isPlaying = true;
    }
    private void ResumeTimer()
    {
        isPlaying = true;
    }
    private void PauseTimer()
    {
        isPlaying = false;
    }
}
