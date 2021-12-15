using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerBehaviour : MonoBehaviour
{
    private const float DialogueDistance = 0.5f;

    [SerializeField] private Vector2 speed = new Vector2(2.0f, 2.0f);

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
    }

    // Update is called once per frame
    private void Update()
    {
        if (!DialogueManager.instance.IsDisplayingDialogue() && TryDialogue())
        {
            return;
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
            if (hit.collider != null &&
                (dialogueBehaviour = hit.collider.gameObject.GetComponent<DialogueBehaviour>()) != null &&
                dialogueBehaviour.GiveDialogue() != null)
            {
                Debug.Log("Hit " + hit.collider.gameObject.name);
                _movement = Vector2.zero;
                DialogueManager.instance.DisplayDialogue(dialogueBehaviour.GiveDialogue());
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