using UnityEngine;

public class LevelSelectBtn : BaseBtn
{
    protected override void OnClick()
    {
        GameStateManager.Instance.ChangeState(GameState.SelectLevel);
    }
}
