using UnityEngine;

public class SetDataBtnLevel : AnMonoBehaviour
{
    [SerializeField] protected LevelBtn levelBtn;
    public LevelBtn LevelBtn => levelBtn;
    [SerializeField] protected LevelSaveData levelSaveData;
    public LevelSaveData LevelSaveData => levelSaveData;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadLevelBtn();
    }
    protected override void Start()
    {
        base.Start();
        this.LoadLevelSaveData();
    }
    protected virtual void LoadLevelBtn()
    {
        if (this.levelBtn != null) return;
        this.levelBtn = transform.GetComponentInParent<LevelBtn>();
    }
    protected virtual void LoadLevelSaveData()
    {
        if (SaveManager.Instance == null) return;
        this.levelSaveData = SaveManager.Instance.GetLevelData(levelBtn.LevelConfig.levelCount);

        bool value = levelSaveData.isUnlocked;
  
        this.SetBtn(value);

        Debug.Log(levelBtn.LevelConfig.levelCount);
    }
    protected virtual void SetBtn(bool value)
    {
        levelBtn.TrySetBtnUnLocked(value);
    }
}
