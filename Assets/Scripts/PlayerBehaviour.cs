using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField]
    private Vector2 speed = new Vector2(2.0f, 2.0f);

    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private Vector2 _movement = Vector2.zero;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        int horizontal = 0;  	//Used to store the horizontal move direction.
        int vertical = 0;		//Used to store the vertical move direction.
			
        //Check if we are running either in the Unity editor or in a standalone build.
			
        //Get input from the input manager, round it to an integer and store in horizontal to set x axis move direction
        horizontal = (int) (Input.GetAxisRaw ("Horizontal"));
			
        //Get input from the input manager, round it to an integer and store in vertical to set y axis move direction
        vertical = (int) (Input.GetAxisRaw ("Vertical"));
			
        //Check if moving horizontally, if so set vertical to zero.
        if(horizontal != 0)
        {
            vertical = 0;
        }

        _movement = new Vector2(speed.x * horizontal, speed.y * vertical);
        SetAnimationAxes(horizontal, vertical);
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = _movement;
        Camera.main.transform.position =
            new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
    }
    
    private void SetAnimationAxes(int horizontal, int vertical)
    {
        _animator.SetInteger("horizontal", horizontal);
        _animator.SetInteger("vertical", vertical);
    }
    

    private void SetAnimation(int horizontal, int vertical)
    {
        if (horizontal > 0)
        {
            _animator.SetTrigger("movedRight");
        } else if (horizontal < 0)
        {
            _animator.SetTrigger("movedLeft");
        } else if (vertical > 0)
        {
            _animator.SetTrigger("movedUp");
        } else if (vertical < 0)
        {
            _animator.SetTrigger("movedDown");
        }
        else
        {
            _animator.SetTrigger("stoppedMoving");
        }
    }
}
