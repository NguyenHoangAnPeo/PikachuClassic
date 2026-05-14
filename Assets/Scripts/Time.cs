using System;
using UnityEngine;

public class Time : BaseGameStateHandler
{
    [SerializeField] protected float timeLeft = 60f;
    public float TimeLeft => timeLeft;

    public event Action<float> OnTimeChanged;

    [SerializeField] protected bool isPlaying;
    private void Update()
    {
        this.RunTimeCoolDown();
    }
    public virtual void SetTimeLeft(float p)
    {
        this.timeLeft = p;
    }
    public bool IsLose()
    {
        return timeLeft == 0;
    }
    protected void RunTimeCoolDown()
    {
        if (!isPlaying) return;
        if (timeLeft > 0)
        {
            timeLeft -= UnityEngine.Time.deltaTime;

            OnTimeChanged?.Invoke(timeLeft);
        }
        else
        {
            GameSessionController.Instance.SetGameState(GameState.Lose);
        }
    }

    protected override void RegisterState()
    {
        stateHandlers.Add(GameState.Start, StartTimer);
        stateHandlers.Add(GameState.Playing, ResumeTimer);
        stateHandlers.Add(GameState.Paused, PauseTimer);
        stateHandlers.Add(GameState.Lose, PauseTimer);
    }
    private void StartTimer()
    {
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
