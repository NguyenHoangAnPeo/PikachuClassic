public class NextLevelBtn : BaseBtn
{
    protected override void OnEnable()
    {
        base.OnEnable();

        if (button == null || LevelManager.Instance == null) return;

        button.interactable = LevelManager.Instance.HasNextLevel();
    }

    protected override void OnClick()
    {
        if (LevelManager.Instance == null) return;

        LevelManager.Instance.PlayNextLevel();
    }
}