using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyComponent : MonoBehaviour
{
    [SerializeField]
    private int _currency;

    public delegate void HandleCurrencyChange(int currency);

    public event HandleCurrencyChange OnCurrencyChange;
    public int Currency { get; private set; }

    void Awake()
    {
        Currency = _currency;
    }

    public void UpdateCurrency(int delta)
    {
        Currency += delta;
        OnCurrencyChange?.Invoke(Currency);
    }
}
