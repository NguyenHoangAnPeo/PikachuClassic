using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseText : MonoBehaviour
{
    [Header("Base Text")]
    [SerializeField] protected TextMeshProUGUI text;
    void Awake()
    {
        this.LoadText();
    }
    protected virtual void LoadText()
    {
        if (this.text != null) return;
        this.text = GetComponent<TextMeshProUGUI>();
        Debug.LogWarning($"[BaseBtn] LoadBtn {this.text.name} in {this.gameObject.name}");
    }
}