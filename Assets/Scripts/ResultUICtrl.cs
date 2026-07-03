using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ResultUICtrl : AnMonoBehaviour,IGameStateListener
{
    [SerializeField] protected List<Transform> winPanel;
    [SerializeField] protected List<Transform> losePanel;

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
        this.HideAllPanels();

        switch (newState)
        {
            case GameState.Lose:
                this.ShowLosePanel();
                break;
            case GameState.Win:
                this.ShowWinPanel();
                break;
        }
    }
    protected void ShowLosePanel()
    {
        foreach(Transform t in losePanel)
        {
            t.gameObject.SetActive(true);
        }
    }
    protected void ShowWinPanel()
    {
        foreach (Transform t in winPanel)
        {
            t.gameObject.SetActive(true);
        }
    }
    protected void HideAllPanels()
    {
        foreach (Transform t in losePanel)
        {
            t.gameObject.SetActive(false);
        }

        foreach (Transform t in winPanel)
        {
            t.gameObject.SetActive(false);
        }
    }
}
