using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(HealthComponent))]
public class EnemyStateController : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private float _maxViewDistance;
    [SerializeField]
    private float _speed;
    private Vector3 _lastPlayerPos;
    private Rigidbody _rb;
    private Ray _ray;
    private HealthComponent _hc;

    private void Awake()
    {
        _lastPlayerPos = transform.position + transform.forward;
        _rb = GetComponent<Rigidbody>();
        _ray = new Ray(transform.position, _player.transform.position - transform.position);
        _hc = GetComponent<HealthComponent>();
        _hc.OnDeath += OnDeath;
        _hc.OnHealthChange += OnHealthChange;
    }

    public void FixedUpdate()
    {
        HandlePlayerTracking();
        HandleMove();
    }

    void HandlePlayerTracking()
    {
        _ray = new Ray(transform.position, _player.transform.position - transform.position);
        RaycastHit hit;
        if (Physics.Raycast(_ray, out hit, _maxViewDistance))
        {
            if (hit.collider.gameObject.layer != 6)
            {
                return;
            }
            _lastPlayerPos = hit.point;
            _lastPlayerPos.y = transform.position.y;
        }
    }

    void OnDeath()
    {
        Destroy(gameObject);
    }

    void OnHealthChange(float health)
    {
        _lastPlayerPos = _player.transform.position;
    }

    void HandleMove()
    {
        var lookDirection = (_lastPlayerPos - transform.position).normalized;
        var lookRotation = Quaternion.LookRotation(lookDirection);
        transform.rotation = lookRotation;
        if (Vector3.Distance(_lastPlayerPos, transform.position) <= .2f)
        {
            return;
        }
        _rb.velocity = transform.forward * _speed * Time.fixedDeltaTime;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _maxViewDistance);
        Gizmos.DrawRay(_ray);
        Gizmos.DrawWireCube(_lastPlayerPos, Vector3.one);
    }
}
