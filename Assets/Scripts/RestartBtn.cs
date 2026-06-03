public class RestartBtn : BaseBtn
{
    protected override void OnClick()
    {
        if (LevelManager.Instance == null) return;

        LevelManager.Instance.ReplayCurrentLevel();
    }
}