using System;
using UnityEngine;

public class CaesarBehaviour : MonoBehaviour
{
    private FightBehaviour _fightBehaviour;
    private Animator _animator;

    private void Awake()
    {
        _fightBehaviour = GetComponent<FightBehaviour>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _fightBehaviour.InitializeState(() => _animator.SetTrigger("slash"), Attacked);
        _fightBehaviour.enabled = false;
    }

    private void Attacked(int newHp)
    {
        if (newHp <= 0)
        {
            _animator.SetTrigger("breakdown");
        }
    }

    public void EnableCaesar()
    {
        _fightBehaviour.enabled = true;
    }
}