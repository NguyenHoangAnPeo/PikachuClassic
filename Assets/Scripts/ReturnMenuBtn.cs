using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnMenuBtn : BaseBtn
{
    protected override void OnClick()
    {
        SceneManager.LoadScene(LevelManager.Instance.menuSceneName);
    }
}
