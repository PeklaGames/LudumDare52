using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageComponent : MonoBehaviour
{
    [SerializeField]
    private float _attackDamage;
    [SerializeField]
    private float _attacksPerSecond;

    public float AttackDamage { get; private set; }
    public float AttacksPerSecond { get; private set; }

    void Awake()
    {
        AttackDamage = _attackDamage;
        AttacksPerSecond = _attacksPerSecond;
    }

    public void UpdateAttackDamage(float delta)
    {
        AttackDamage += delta;
    }

    public void UpdateAttacksPerSecond(float delta)
    {
        AttacksPerSecond += delta;
    }
}
