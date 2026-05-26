using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnMonoBehaviour : MonoBehaviour
{
    protected virtual void Reset()
    {
        Debug.Log("Ham reset da dc chay");
        this.LoadComponents();
    }
    protected virtual void Start()
    {
        //For override
    }
    protected virtual void Awake()
    {
        this.LoadComponents();
        this.ResetValue();
    }
    protected virtual void LoadComponents()
    {
        // For override
    }
    protected virtual void ResetValue()
    {
        // For override
    }
    protected virtual void OnEnable()
    {
        //For override
    }
}