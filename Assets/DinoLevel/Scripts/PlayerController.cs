using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    [Header("Player Movement")]
    [SerializeField] private float speed = 1;
    [SerializeField] private float jumpForce = 5;
   // [SerializeField] private int extraJumps = 2;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;  //game object for the middlepoint 
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask whatIsGround; //mask to detect if in radius of groundCheck 

    [Header("Count")]
    [SerializeField] private UnityEngine.UI.Text countText;

    private bool _facingRight = true;
    private int _jumpCount = 0;
   

    private bool IsGrounded => Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);


    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            _rigidbody.velocity = Vector2.up * jumpForce;
            _jumpCount++;
        }


        //if (IsGrounded)
        //{
        //    _jumpCount = 0;
        //}

        //if (Input.GetButtonDown("Jump") && _jumpCount < extraJumps)
        //{
        //    _rigidbody.velocity = Vector2.up * jumpForce;
        //    _jumpCount++;
        //}
            
    }

    private void FixedUpdate()
    {

        var movement = Input.GetAxis("Horizontal"); 
        Debug.Log(movement); 

        _rigidbody.velocity = new Vector2(movement * speed, _rigidbody.velocity.y);

        if (!_facingRight && movement > 0)
            Flip();
        else if (_facingRight && movement < 0)
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


    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (!collision.CompareTag("Coin"))
    //        return;

    //    collision.gameObject.SetActive(false); //just deactivate
    //    //Destroy(collision.gameObject);
    //    coinCount++;

    //    countText.text = $"Coins: {coinCount}";
    //}


}
