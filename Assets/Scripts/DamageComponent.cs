using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageComponent : MonoBehaviour
{
    [SerializeField]
    private float _attackDamage;
    [SerializeField]
    private int _attacksPerSecond;

    public float AttackDamage { get; private set; }
    public int AttacksPerSecond { get; private set; }

    void Awake()
    {
        AttackDamage = _attackDamage;
        AttacksPerSecond = _attacksPerSecond;
    }

    public void UpdateAttackDamage(float delta)
    {
        AttackDamage += delta;
    }

    public void UpdateAttacksPerSecond(int delta)
    {
        AttacksPerSecond += delta;
    }
}
