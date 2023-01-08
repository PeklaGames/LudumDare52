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

    void Awake()
    {
        CurrentHealth = _maxHealth;
    }

    void Update()
    {
        if (CurrentHealth <= 0)
        {
            OnDeath?.Invoke();
        }
    }

    public void UpdateHealth(float delta)
    {
        CurrentHealth += delta;
        OnHealthChange?.Invoke(CurrentHealth);
    }
}
