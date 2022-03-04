using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Animator _animator;

    [Header("Player Movement")] [SerializeField]
    private float speed = (float)5.2;

    [SerializeField] private float jumpForce = (float) 6.7;
    [SerializeField] private int extraJumps = 1;
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip dieClip;

    [Header("Ground Check")] [SerializeField]
    private Transform groundCheck; //game object for the middlepoint 

    [SerializeField] private float checkRadius = (float) 0.02;
    [SerializeField] private LayerMask whatIsGround; //mask to detect if in radius of groundCheck +

    [SerializeField] private RuntimeAnimatorController[] boyGirlAnimator;
    // [Header("Count")]
    // [SerializeField] private UnityEngine.UI.Text countText;

    private float _movement = 0;
    private float _jumpMovement = 0;
    private bool _facingRight = true;
    private int _jumpCount = 0;
    private bool _isJumping = false;


    private bool IsGrounded => Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);


    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        if (GameState.instance.selectedCharacterIdx < 2)
            _animator.runtimeAnimatorController = boyGirlAnimator[0];
        else
            _animator.runtimeAnimatorController = boyGirlAnimator[1];
    }

    // Update is called once per frame
    void Update()
    {
        if (DialogueManager.instance.IsDisplayingDialogue() || IngameMenuBehaviour.instance.IsMenuActive())
        {
            _animator.SetTrigger("idle");
            _movement = 0;
            _jumpMovement = 0;
            return;
        }
        if (IsGrounded && _jumpMovement == 0)
        {
            _jumpCount = 0;
            _isJumping = false;
        }

        if ((Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire1")) && _jumpCount < extraJumps)
        {
            _jumpMovement = jumpForce;
            _jumpCount++;
            _isJumping = true;
            _animator.SetTrigger("jump1");
            SoundManager.instance.GetEffectSource().PlayOneShot(jumpClip);
        }

        _movement = Input.GetAxis("Horizontal");
        //Debug.Log(_movement); 
        if (_movement == 0 && _isJumping == false)
            _animator.SetTrigger("idle");
        else if (_movement != 0 && _isJumping == false)
            _animator.SetTrigger("run");

        _movement *= speed;

        if (!_facingRight && _movement > 0)
            Flip();
        else if (_facingRight && _movement < 0)
            Flip();
    }

    private void FixedUpdate()
    {
        float vertical;
        if (Math.Abs(_jumpMovement) > 0)
        {
            vertical = _jumpMovement;
            _jumpMovement = 0;
        }
        else
        {
            vertical = _rigidbody.velocity.y;
        }

        _rigidbody.velocity = new Vector2(_movement, vertical);

        Camera.main.transform.position =
            new Vector3(transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        var scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }


    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Killing"))
        {
            _animator.SetTrigger("dead");
            _rigidbody.simulated = false;
            SoundManager.instance.GetEffectSource().PlayOneShot(dieClip);
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene("Dino_Escape");
        }


        //collision.gameObject.SetActive(false); //just deactivate
        //Destroy(collision.gameObject);
    }

    private IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Killing"))
        {
            _animator.SetTrigger("dead");
            _rigidbody.simulated = false;
            SoundManager.instance.GetEffectSource().PlayOneShot(dieClip);
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene("Dino_Escape");
        }
    }
}