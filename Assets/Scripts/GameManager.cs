using UnityEngine;

public class GameManager : AnMonoBehaviour
{
    [SerializeField] protected static GameManager instance;
    public static GameManager Instance => instance;

    [SerializeField] protected LevelConfig currentLevel;
    public LevelConfig CurrentLevel => currentLevel;

    protected override void Awake()
    {
        base.Awake();
        if(GameManager.instance != null)
        {
            Destroy(gameObject);
            return;
        }
        GameManager.instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public virtual void SetCurrentLevel(LevelConfig level)
    {
        this.currentLevel = level;
    }
}
