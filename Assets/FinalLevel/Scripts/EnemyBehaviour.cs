using System;
using UnityEngine;

class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private float speed = 2;
    [SerializeField] private float attackStartDelay = 0.5f;
    [SerializeField] private bool equalizeYFirst = true;
    [SerializeField] private float sameLineThreshold = 0.2f;
    [SerializeField] private float attackHitDelay = 1;
    [SerializeField] private Vector2 initialDirection = Vector2.down;

    [SerializeField] private float disableDelay = 0.6f;

    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private Transform _player;
    private FightBehaviour _fightBehaviour;
    private Vector2 _movement = Vector2.zero;

    private bool defeated;
    private float disableTime;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _player = GameObject.Find("Player").GetComponent<Transform>();
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
        if (defeated)
        {
            return;
        }
        
        if (newHp <= 0)
        {
            _animator.SetTrigger("breakdown");
            disableTime = disableDelay;
            _movement = Vector2.zero;
            defeated = true;
        }
    }

    private void Update()
    {
        if (defeated)
        {
            disableTime -= Time.deltaTime;
            if (disableTime <= 0)
            {
                gameObject.SetActive(false);
            }
            return;
        }
        
        if (DialogueManager.instance.IsDisplayingDialogue() || IngameMenuBehaviour.instance.IsMenuActive())
        {
            _movement = Vector2.zero;
            return;
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
            _movement = secondAxisOwn > secondAxisPlayer ? -secondAxisMovement : secondAxisMovement;
        }
        else
        {
            _movement = firstAxisOwn > firstAxisPlayer ? -firstAxisMovement : firstAxisMovement;
        }
    }

    private void FixedUpdate()
    {
        if (Math.Abs(_movement.x) <= float.Epsilon && Math.Abs(_movement.y) <= float.Epsilon)
        {
            _rigidbody.velocity = Vector2.zero;
        }
        else
        {
            _rigidbody.velocity = _movement;   
        }
        _animator.SetInteger("horizontal", Math.Sign(_movement.x));
        _animator.SetInteger("vertical", Math.Sign(_movement.y));
    }
}