using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CurrencyComponent), typeof(RandomSoundPlayer))]
public class ToothController : MonoBehaviour
{
    [SerializeField]
    private float _initialForce;
    private Rigidbody _rb;
    private CurrencyComponent _currency;
    private RandomSoundPlayer _soundPlayer;
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _currency = GetComponent<CurrencyComponent>();
        _soundPlayer = GetComponent<RandomSoundPlayer>();
    }

    void Start()
    {
        _rb.AddExplosionForce(_initialForce, transform.position + Vector3.down, 3f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            _soundPlayer.PlayRandomSound();
            GameStateManager.Instance.Currency.UpdateCurrency(_currency.Currency);
            gameObject.GetComponent<Collider>().enabled = false;
            Destroy(transform.GetChild(0).gameObject);
            StartCoroutine(DestroyAfterTime());
        }
    }
    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
