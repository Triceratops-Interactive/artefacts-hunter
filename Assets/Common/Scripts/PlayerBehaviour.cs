using System;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private float DialogueDistance = 0.5f;

    [SerializeField] public Vector2 speed = new Vector2(2.0f, 2.0f);

    [SerializeField] private bool fightMode = false;


    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private BoxCollider2D _boxCollider;
    private Vector2 _movement = Vector2.zero;
    private Vector2 _facingDirection = new Vector2(0, 1); // facing up by default

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();

        if (!fightMode)
        {
            _animator.runtimeAnimatorController =
                GameState.instance.ingameAnimators[GameState.instance.selectedCharacterIdx]; // Set correct character
        }
    }

    private void Update()
    {
        if (IngameMenuBehaviour.instance != null && IngameMenuBehaviour.instance.IsMenuActive())
        {
            _movement = Vector2.zero;
            SetAnimationAxes(0, 0);
            return;
        }

        if (fightMode)
        {
            var slashPressed = Input.GetButtonDown("Fire1");
            if (slashPressed && !DialogueManager.instance.IsDisplayingDialogue())
            {
                _animator.SetTrigger("slash");
            }
        }
        else
        {
            if (!DialogueManager.instance.IsDisplayingDialogue() && TryDialogue())
            {
                return;
            }
        }


        Move();
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = _movement;
        if (Math.Abs(_movement.x) > float.Epsilon || Math.Abs(_movement.y) > float.Epsilon)
        {
            _facingDirection = new Vector2(_movement.x, _movement.y);
        }

        Camera.main.transform.position =
            new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
    }

    private void SetAnimationAxes(int horizontal, int vertical)
    {
        _animator.SetInteger("horizontal", horizontal);
        _animator.SetInteger("vertical", vertical);
    }

    private bool TryDialogue()
    {
        var interactPressed = Input.GetButtonDown("Fire1");
        if (interactPressed)
        {
            _boxCollider.enabled = false;
            var hit = Physics2D.Raycast(transform.position, _facingDirection, DialogueDistance);
            _boxCollider.enabled = true;
            DialogueBehaviour dialogueBehaviour;
            DialogueElement[] dialogue;
            Action callback;
            if (hit.collider == null ||
                (dialogueBehaviour = hit.collider.gameObject.GetComponent<DialogueBehaviour>()) == null)
            {
                return false;
            }

            (dialogue, callback) = dialogueBehaviour.GiveDialogue();

            if (dialogue != null)
            {
                _movement = Vector2.zero;
                DialogueManager.instance.DisplayDialogue(dialogue, callback);
                return true;
            }
        }

        return false;
    }

    private void Move()
    {
        var horizontal = 0;
        var vertical = 0;

        if (!DialogueManager.instance.IsDisplayingDialogue())
        {
            horizontal = (int) (Input.GetAxisRaw("Horizontal"));
            vertical = (int) (Input.GetAxisRaw("Vertical"));
            if (horizontal != 0)
            {
                vertical = 0;
            }
        }

        _movement = new Vector2(speed.x * horizontal, speed.y * vertical);
        SetAnimationAxes(horizontal, vertical);
    }
}