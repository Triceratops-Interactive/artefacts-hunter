using System;
using UnityEngine;

public class FightBehaviour : MonoBehaviour
{
    [SerializeField] private int hp = 1;
    [SerializeField] private int attackPower = 1;
    [SerializeField] private float attackHitDelay = 0.517f;
    [SerializeField] private float attackDistance = 0.4f;

    private BoxCollider2D _boxCollider;
    private Action _attackAnimation;
    private Action<int> _attackedCallback;
    
    // Needed for one attack
    private float _attackTimer = 0.0f;
    private Vector2 _attackDirection = Vector2.zero;
    private bool _attacking = false;

    public void InitializeState(Action attackAnimation, Action<int> attackedCallback)
    {
        this._attackAnimation = attackAnimation;
        this._attackedCallback = attackedCallback;
    }

    public void StartAttack(Vector2 attackDirection)
    {
        _attackDirection = attackDirection;
        _attackAnimation?.Invoke();
        _attackTimer = attackHitDelay;
        _attacking = true;
    }

    public bool IsAttacking()
    {
        return _attacking;
    }

    public void GetAttacked(int power)
    {
        hp -= power;

        _attackedCallback?.Invoke(hp);
    }
    
    private void Attack()
    {
        _boxCollider.enabled = false;
        var hit = Physics2D.Raycast(transform.position, _attackDirection, attackDistance);
        _boxCollider.enabled = true;
        FightBehaviour attackedFighter;
        if (hit.collider == null || (attackedFighter = hit.collider.gameObject.GetComponent<FightBehaviour>()) == null || !attackedFighter.enabled)
        {
            _attacking = false;
            return;
        }
        
        attackedFighter.GetAttacked(attackPower);
        _attacking = false;
    }

    private void Update()
    {
        if (!_attacking)
        {
            return;
        }

        _attackTimer -= Time.deltaTime;

        if (_attackTimer <= 0)
        {
            Attack();
        }
    }

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
    }
}