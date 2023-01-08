using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(HealthComponent), typeof(DamageComponent))]
[RequireComponent(typeof(RandomSoundPlayer))]
public class EnemyStateController : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;
    [Header("Movement Attributes")]
    [SerializeField]
    private float _maxViewDistance;
    [SerializeField]
    private float _speed;
    [Header("Attack Attributes")]
    [SerializeField]
    private float _maxAttackRange;
    [Header("Drop Attributes")]
    [SerializeField]
    private int _dropCount;
    [SerializeField]
    private GameObject _dropPrefab;

    private float _attackTimer = 0f;
    private Vector3 _lastPlayerPos;
    private Rigidbody _rb;
    private Ray _ray;
    private HealthComponent _hc;
    private DamageComponent _dc;
    private RandomSoundPlayer _soundPlayer;
    private State _state = State.IDLE;

    private void Awake()
    {
        _lastPlayerPos = transform.position + transform.forward;
        _rb = GetComponent<Rigidbody>();
        _ray = new Ray(transform.position, _player.transform.position - transform.position);
        _hc = GetComponent<HealthComponent>();
        _dc = GetComponent<DamageComponent>();
        _soundPlayer = GetComponent<RandomSoundPlayer>();
        _hc.OnDeath += OnDeath;
        _hc.OnHealthChange += OnHealthChange;
        _attackTimer = 1f / _dc.AttacksPerSecond;
        GateController.EnemyCount += 1;
    }

    public void FixedUpdate()
    {
        HandlePlayerTracking();
        switch (_state)
        {
            case State.CHASING:
                HandleMove();
                break;
            case State.ATTACKING:
                HandleAttack();
                break;
            default:
                break;
        }
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
        if (Vector3.Distance(transform.position, _player.transform.position) <= _maxAttackRange)
        {
            _state = State.ATTACKING;
        }
        else if (Vector3.Distance(transform.position, _lastPlayerPos) >= _maxAttackRange)
        {
            _state = State.CHASING;
        }
        else
        {
            _state = State.IDLE;
        }
    }

    void OnDeath()
    {
        for (int d = 0; d < _dropCount; d++)
        {
            Instantiate(_dropPrefab, transform.position, Quaternion.identity);
        }

        GateController.EnemyCount -= 1;
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
        if (Vector3.Distance(_lastPlayerPos, transform.position) <= _maxAttackRange - .2f)
        {
            return;
        }
        _rb.velocity = transform.forward * _speed * Time.fixedDeltaTime;
    }

    void HandleAttack()
    {
        if (_attackTimer >= 1f / _dc.AttacksPerSecond)
        {
            _soundPlayer.PlayRandomSound();
            GameStateManager.Instance.Health.UpdateHealth(-_dc.AttackDamage);
            _attackTimer = 0;
        }
        _attackTimer += Time.fixedDeltaTime;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _maxViewDistance);
        Gizmos.DrawRay(_ray);
        Gizmos.DrawWireCube(_lastPlayerPos, Vector3.one);
    }

    private enum State
    {
        IDLE,
        CHASING,
        ATTACKING
    }
}
