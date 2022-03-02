using System;
using UnityEngine;
using UnityEngine.Events;

class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private float speed = 2;
    [SerializeField] private float attackStartDelay = 0.5f;
    [SerializeField] private float attackStartDistance = 0.3f;
    [SerializeField] private bool equalizeYFirst = true;
    [SerializeField] private float sameLineThreshold = 0.2f;
    [SerializeField] private Vector2 initialDirection = Vector2.down;
    [SerializeField] private AudioClip slashClip;

    [SerializeField] private float disableDelay = 0.6f;
    [SerializeField] private UnityEvent defeatedCallback;

    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private Transform _player;
    private PlayerBehaviour _playerBehaviour;
    private FightBehaviour _fightBehaviour;
    private Vector2 _movement = Vector2.zero;

    private bool _attackStarting;
    private float _attackStartingTime;
    private Vector2 _attackDirection;

    private bool _defeated;
    private float _disableTime;
    private bool _callbackCalled;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _player = GameObject.Find("Player").GetComponent<Transform>();
        _playerBehaviour = GameObject.Find("Player").GetComponent<PlayerBehaviour>();
        _fightBehaviour = GetComponent<FightBehaviour>();
    }

    private void Start()
    {
        SetInitialPosition();
        _fightBehaviour.InitializeState(() => _animator.SetTrigger("slash"), Attacked);
    }

    private void SetInitialPosition()
    {
        if (Math.Abs(initialDirection.x) > float.Epsilon)
        {
            _animator.Play(initialDirection.x < 0 ? "PlayerMoveLeft" : "PlayerMoveRight");
        }
        else
        {
            _animator.Play(initialDirection.y < 0 ? "PlayerMoveDown" : "PlayerMoveUp");
        }
    }

    private void Attacked(int newHp)
    {
        if (_defeated)
        {
            return;
        }

        if (newHp <= 0)
        {
            _animator.SetTrigger("breakdown");
            _disableTime = disableDelay;
            _movement = Vector2.zero;
            _defeated = true;
        }
    }

    private void Update()
    {
        if (DialogueManager.instance.IsDisplayingDialogue() || IngameMenuBehaviour.instance.IsMenuActive())
        {
            _movement = Vector2.zero;
            return;
        }

        if (_defeated)
        {
            _disableTime -= Time.deltaTime;
            if (_disableTime > 0) return;

            if (defeatedCallback.GetPersistentEventCount() > 0)
            {
                if (!_callbackCalled)
                {
                    _callbackCalled = true;
                    defeatedCallback.Invoke();
                }
            }
            else
            {
                gameObject.SetActive(false);
            }

            return;
        }

        if (_fightBehaviour.IsAttacking() || _playerBehaviour.IsDefeated())
        {
            _movement = Vector2.zero;
            return;
        }

        if (_attackStarting)
        {
            _attackStartingTime -= Time.deltaTime;
            if (_attackStartingTime > 0) return;

            if (slashClip != null)
            {
                SoundManager.instance.GetEffectSource().PlayOneShot(slashClip);
            }
            _fightBehaviour.StartAttack(_attackDirection);
            _attackStarting = false;
        }

        float firstAxisOwn, firstAxisPlayer, secondAxisOwn, secondAxisPlayer;
        Vector2 firstAxisMovement, secondAxisMovement;
        if (equalizeYFirst)
        {
            firstAxisOwn = transform.position.y;
            firstAxisPlayer = _player.position.y;
            secondAxisOwn = transform.position.x;
            secondAxisPlayer = _player.position.x;
            firstAxisMovement = new Vector2(0, speed);
            secondAxisMovement = new Vector2(speed, 0);
        }
        else
        {
            firstAxisOwn = transform.position.x;
            firstAxisPlayer = _player.position.x;
            secondAxisOwn = transform.position.y;
            secondAxisPlayer = _player.position.y;
            firstAxisMovement = new Vector2(speed, 0);
            secondAxisMovement = new Vector2(0, speed);
        }

        if (Math.Abs(firstAxisOwn - firstAxisPlayer) <= sameLineThreshold)
        {
            if (Math.Abs(secondAxisOwn - secondAxisPlayer) <= attackStartDistance)
            {
                StartAttack();
                return;
            }

            _movement = secondAxisOwn > secondAxisPlayer ? -secondAxisMovement : secondAxisMovement;
        }
        else
        {
            if (Math.Abs(secondAxisOwn - secondAxisPlayer) <= sameLineThreshold &&
                Math.Abs(firstAxisOwn - firstAxisPlayer) <= attackStartDistance)
            {
                StartAttack();
                return;
            }

            _movement = firstAxisOwn > firstAxisPlayer ? -firstAxisMovement : firstAxisMovement;
        }
    }

    private void StartAttack()
    {
        if (Math.Abs(_rigidbody.velocity.x) > float.Epsilon || Math.Abs(_rigidbody.velocity.y) > float.Epsilon)
        {
            _attackDirection = _rigidbody.velocity;
        }
        _movement = Vector2.zero;
        _attackStartingTime = attackStartDelay;
        _attackStarting = true;
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = _movement;
        _animator.SetInteger("horizontal", Math.Sign(_movement.x));
        _animator.SetInteger("vertical", Math.Sign(_movement.y));
    }
}