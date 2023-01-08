using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField]
    private float _maxHealth;

    public delegate void HandleOnDeath();
    public delegate void HandleHealthChange(float health);

    public event HandleOnDeath OnDeath;
    public event HandleHealthChange OnHealthChange;
    public float CurrentHealth { get; private set; }
    public float MaxHealth { get; private set; }

    void Awake()
    {
        CurrentHealth = _maxHealth;
        MaxHealth = _maxHealth;
    }

    void Update()
    {
        if (CurrentHealth <= 0)
        {
            OnDeath?.Invoke();
        }
    }

    public bool UpdateHealth(float delta)
    {
        if (CurrentHealth > MaxHealth)
        {
            return false;
        }
        CurrentHealth = Mathf.Clamp(CurrentHealth + delta, 0, MaxHealth);
        OnHealthChange?.Invoke(CurrentHealth);
        return true;
    }
    public void UpdateMaxHealth(float delta)
    {
        MaxHealth += delta;
    }
}
