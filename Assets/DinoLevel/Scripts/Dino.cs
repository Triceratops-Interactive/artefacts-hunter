using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dino : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Animator _animator;

    [Header("Dino Movement")]
    [SerializeField] private float speed = 7;
    [SerializeField] private float jumpForce = 6;


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
        //_movement = Input.GetAxis("Horizontal"); 
        
        _rigidbody.velocity = new Vector2(speed, _rigidbody.velocity.y);
        
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dino_Killing"))
        {
            _animator.SetTrigger("dead");
            speed = 0;
        }
        else if (collision.CompareTag("Jump"))
        {
            _animator.SetTrigger("jump");
            _rigidbody.velocity = Vector2.up * jumpForce;
        }
            

        //collision.gameObject.SetActive(false); //just deactivate
        //Destroy(collision.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
            Destroy(collision.gameObject);
    }
}
