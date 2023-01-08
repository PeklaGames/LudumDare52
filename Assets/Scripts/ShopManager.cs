using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _shopCanvas;

    void Awake()
    {
        _shopCanvas.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 6)
        {
            return;
        }
        _shopCanvas.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != 6)
        {
            return;
        }
        _shopCanvas.SetActive(false);
    }

    private bool UpdateCurrency(int cost)
    {
        if (GameStateManager.Instance.Currency.Currency >= cost)
        {
            GameStateManager.Instance.Currency.UpdateCurrency(-cost);
            return true;
        }
        return false;
    }
    public void Heal()
    {
        if (GameStateManager.Instance.Health.CurrentHealth >= GameStateManager.Instance.Health.MaxHealth || !UpdateCurrency(1))
        {
            return;
        }
        GameStateManager.Instance.Health.UpdateHealth(1);
    }

    public void AddMaxHealth()
    {
        if (!UpdateCurrency(5))
        {
            return;
        }
        GameStateManager.Instance.Health.UpdateMaxHealth(5);
    }

    public void AddDamage()
    {
        if (!UpdateCurrency(5))
        {
            return;
        }
        GameStateManager.Instance.Damage.UpdateAttackDamage(1);
    }

    public void AddShots()
    {
        if (!UpdateCurrency(5))
        {
            return;
        }
        GameStateManager.Instance.Damage.UpdateAttacksPerSecond(1);
    }
}
