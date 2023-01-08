using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof (HealthComponent), typeof(CurrencyComponent), typeof(DamageComponent))]
public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }

    [SerializeField]
    private TextMeshProUGUI _healthText;
    [SerializeField]
    private TextMeshProUGUI _currencyText;

    public HealthComponent Health { get;  private set; }
    public CurrencyComponent Currency { get; private set; }
    public DamageComponent Damage { get; private set; }

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
        Damage = GetComponent<DamageComponent>();
        HandleHealthChange(Health.CurrentHealth);
        UpdateCurrencyText(Currency.Currency);
        Health.OnHealthChange += HandleHealthChange;
        Currency.OnCurrencyChange += UpdateCurrencyText;
    }


    private void HandleHealthChange(float health)
    {
        if (health <= 0)
        {
            SceneManager.LoadScene("Menu");
        }
        _healthText.text = health.ToString("0.0");
    }

    private void UpdateCurrencyText(int currency)
    {
        _currencyText.text = currency.ToString();
    }
}
