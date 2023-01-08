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
        SceneManager.sceneLoaded += OnSceneLoaded;
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
        Health.OnHealthChange += HandleHealthChange;
        Currency.OnCurrencyChange += UpdateCurrencyText;
    }

    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        if (scene.buildIndex == 0)
        {
            var temp = Instance.gameObject;
            Instance = null;
            Destroy(temp);
        }
    }

    private void Start()
    {
        HandleHealthChange(Health.CurrentHealth);
        UpdateCurrencyText(Currency.Currency);
    }

    private void HandleHealthChange(float health)
    {
        if (health <= 0)
        {
            SceneManager.LoadScene(0);
        }
        _healthText.text = health.ToString("0.0");
    }

    private void UpdateCurrencyText(int currency)
    {
        _currencyText.text = currency.ToString();
    }
}
