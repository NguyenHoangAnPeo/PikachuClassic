using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelBtn : BaseBtn
{
    [SerializeField] protected LevelConfig levelConfig;
    public LevelConfig LevelConfig => levelConfig;
    protected override void OnClick()
    {
        if (this.levelConfig == null) return;

        LevelManager.Instance.PlayLevel(this.levelConfig);
    }
    public void TrySetBtnUnLocked(bool value)
    {
        button.interactable = value;
        Debug.Log("Da set btn = " + value);
    }
}
