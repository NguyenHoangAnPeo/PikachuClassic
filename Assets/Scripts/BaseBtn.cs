using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseBtn : AnMonoBehaviour
{
    [Header("Base Btn")]
    [SerializeField] protected Button button;

    protected override void Start()
    {
        base.Start();
        this.AddOnclickEvent();
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBtn();
    }
    protected virtual void LoadBtn()
    {
        if (this.button != null) return;
        this.button = GetComponent<Button>();
        Debug.LogWarning($"[BaseBtn] LoadBtn {this.button.name} in {this.gameObject.name}");
    }
    protected virtual void AddOnclickEvent()
    {
        this.button.onClick.AddListener(this.OnClick);
    }
    protected abstract void OnClick();
}