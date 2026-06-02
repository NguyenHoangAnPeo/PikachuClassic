using System;
using UnityEngine;

public class Time : AnMonoBehaviour,IGameStateListener
{
    [SerializeField] protected float timeLeft = 60f;
    public float TimeLeft => timeLeft;

    protected float timeMax;

    public event Action<float> OnTimeChanged;

    [SerializeField] protected bool isPlaying;
    protected override void OnEnable()
    {
        GameStateManager.Instance.RegisterListener(this);
    }

    private void OnDisable()
    {
        GameStateManager.Instance.UnregisterListener(this);
    }
    protected override void Start()
    {
        if(GameStateManager.Instance == null)return;

        GameStateManager.Instance.ChangeState(GameState.Playing);
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
            GameStateManager.Instance.ChangeState(GameState.Lose);
        }
    }
    protected virtual void SetDataLevel()
    {
        var currentLevel = GameManager.Instance.CurrentLevel;

        this.SetTimeLeft(currentLevel.timeLimit);
    }
    public void OnGameStateChanged(GameState newState)
    {
        switch (newState)
        {
            case GameState.Playing:
                this.StartTimer();
                break;
            case GameState.Paused:
                this.PauseTimer();
                break;
        }
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
