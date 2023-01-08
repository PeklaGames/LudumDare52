using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class GateController : MonoBehaviour
{
    public static int EnemyCount = 0;

    [SerializeField]
    private bool _locked = true;

    [SerializeField]
    private SceneLoadTrigger _sceneLoader;

    private Animator _animator;
    private int _lockedAnim = Animator.StringToHash("locked");

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _sceneLoader.enabled = false;
        _animator.SetBool(_lockedAnim, _locked);
    }

    private void FixedUpdate()
    {
        if (EnemyCount <= 0)
        {
            Unlock();
        }
    }

    void Unlock()
    {
        _locked = false;
        _sceneLoader.enabled = true;
        _animator.SetBool(_lockedAnim, _locked);
    }
}
