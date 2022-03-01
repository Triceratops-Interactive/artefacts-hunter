using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Animator _animator;

    [Header("Player Movement")]
    [SerializeField] private float speed = 9;
    [SerializeField] private float jumpForce = 6;
    [SerializeField] private int extraJumps = 1;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;  //game object for the middlepoint 
    [SerializeField] private float checkRadius = (float)0.02;
    [SerializeField] private LayerMask whatIsGround; //mask to detect if in radius of groundCheck 

    [Header("Count")]
    [SerializeField] private UnityEngine.UI.Text countText;

    private float _movement = 0;
    private bool _facingRight = true;
    private int _jumpCount = 0;
    private bool _isJumping = false;
   

    private bool IsGrounded => Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);


    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        

        

        //if (Input.GetButtonDown("Jump") && _jumpCount < extraJumps)
        //{
        //    _rigidbody.velocity = Vector2.up * jumpForce;
        //    _jumpCount++;
        //}
            
    }

    private void FixedUpdate()
    {
        if (IsGrounded)
        {
            _jumpCount = 0;
           _isJumping = false;
        }
        
        if (Input.GetButtonDown("Jump") && _jumpCount < extraJumps)
        {
            _rigidbody.velocity = Vector2.up * jumpForce;
            _jumpCount++;
            _isJumping = true;
            _animator.SetTrigger("jump1");
            //_animator.SetBool("jump_bool", _isJumping);
        }

        _movement = Input.GetAxis("Horizontal"); 
        //Debug.Log(_movement); 
        if(_movement == 0 && _isJumping == false)
            _animator.SetTrigger("idle");
        else if(_movement != 0 && _isJumping == false)
            _animator.SetTrigger("run");
        
        _rigidbody.velocity = new Vector2(_movement * speed, _rigidbody.velocity.y);

        if (!_facingRight && _movement > 0)
            Flip();
        else if (_facingRight && _movement < 0)
            Flip();

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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Killing"))
            _animator.SetTrigger("dead");

        //collision.gameObject.SetActive(false); //just deactivate
        //Destroy(collision.gameObject);
    }


}
