using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : AnMonoBehaviour
{
    [SerializeField] protected static LevelManager instance;
    public static LevelManager Instance => instance;

    [SerializeField] protected List<LevelConfig> levels = new List<LevelConfig>();
    public List<LevelConfig> Levels => levels;

    [SerializeField] protected LevelConfig currentLevel;
    public LevelConfig CurrentLevel => currentLevel;

    public string gameSceneName = "GameScene";
    public string menuSceneName = "Menu";
    protected override void Awake()
    {
        base.Awake();
        if (LevelManager.instance != null)
        {
            Destroy(gameObject);
            return;
        }
        LevelManager.instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public virtual void SetCurrentLevel(LevelConfig level)
    {
        this.currentLevel = level;
    }
    public virtual void PlayLevel(LevelConfig level)
    {
        if (level == null) return;

        this.SetCurrentLevel(level);
        this.LoadGameScene();
    }

    public virtual void ReplayCurrentLevel()
    {
        if (currentLevel == null) return;

        this.LoadGameScene();
    }

    public virtual void PlayNextLevel()
    {
        if (!TryGetNextLevel(out LevelConfig nextLevel)) return;

        this.PlayLevel(nextLevel);
    }

    public virtual void LoadMenuScene()
    {
        SceneManager.LoadScene(menuSceneName);
    }

    public virtual bool HasNextLevel()
    {
        return TryGetNextLevel(out _);
    }

    protected virtual bool TryGetNextLevel(out LevelConfig nextLevel)
    {
        nextLevel = null;

        if (currentLevel == null) return false;
        if (levels == null || levels.Count == 0) return false;

        foreach (LevelConfig level in levels)
        {
            if (level == null) continue;
            if (level.levelCount != currentLevel.levelCount + 1) continue;

            nextLevel = level;
            return true;
        }

        return false;
    }

    protected virtual void LoadGameScene()
    {
        SceneManager.LoadScene(gameSceneName);
    }
}
