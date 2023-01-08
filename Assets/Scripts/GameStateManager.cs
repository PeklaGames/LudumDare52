using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof (HealthComponent), typeof (CurrencyComponent))]
public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }

    [SerializeField]
    private TextMeshProUGUI _healthText;
    [SerializeField]
    private TextMeshProUGUI _currencyText;

    public HealthComponent Health { get;  private set; }
    public CurrencyComponent Currency { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
        Health = GetComponent<HealthComponent>();
        Currency = GetComponent<CurrencyComponent>();
        UpdateHealthText(Health.CurrentHealth);
        UpdateCurrencyText(Currency.Currency);
        Health.OnHealthChange += UpdateHealthText;
        Currency.OnCurrencyChange += UpdateCurrencyText;
    }


    private void UpdateHealthText(float health)
    {
        _healthText.text = health.ToString("0");
    }

    private void UpdateCurrencyText(int currency)
    {
        _currencyText.text = currency.ToString();
    }
}
