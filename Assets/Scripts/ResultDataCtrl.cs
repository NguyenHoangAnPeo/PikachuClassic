using UnityEngine;

public class ResultDataCtrl : AnMonoBehaviour, IGameStateListener
{
    [SerializeField] protected LevelConfig levelConfig;

    protected override void Awake()
    {
        base.Awake();
        this.LoadComponents();
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadLevelConfig();
    }
    protected virtual void LoadLevelConfig()
    {
        if (this.levelConfig != null) return;
        this.levelConfig = LevelManager.Instance.CurrentLevel;
    }
    protected override void OnEnable()
    {
        GameStateManager.Instance.RegisterListener(this);
    }

    private void OnDisable()
    {
        GameStateManager.Instance.UnregisterListener(this);
    }
    public void OnGameStateChanged(GameState newState)
    {
        this.SetMatchResult();
    }
    protected virtual void SetMatchResult()
    {
        if (this.levelConfig == null) return;

    }
}
