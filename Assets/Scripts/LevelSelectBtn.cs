using UnityEngine;

public class LevelSelectBtn : BaseBtn
{
    protected override void OnClick()
    {
        GameSessionController.Instance.SetGameState(GameState.SelectLevel);
    }
}
