using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CurrencyComponent))]
public class ToothController : MonoBehaviour
{
    [SerializeField]
    private float _initialForce;
    private Rigidbody _rb;
    private CurrencyComponent _currency;
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _currency = GetComponent<CurrencyComponent>();
    }

    void Start()
    {
        _rb.AddExplosionForce(_initialForce, transform.position + Vector3.down, 3f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            GameStateManager.Instance.Currency.UpdateCurrency(_currency.Currency);
            Destroy(gameObject);
        }
    }
}
