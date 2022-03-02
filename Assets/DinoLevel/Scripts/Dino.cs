using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dino : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private AudioSource _audio;
    private bool _startedAudio;

    [Header("Dino Movement")] [SerializeField]
    private float speed = 4;

    [SerializeField] private float jumpForce = 6;
    [SerializeField] private AudioClip destroyClip;


    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        //_movement = Input.GetAxis("Horizontal"); 
        if (DialogueManager.instance.IsDisplayingDialogue() || IngameMenuBehaviour.instance.IsMenuActive())
        {
            _rigidbody.velocity = Vector2.zero;
            return;
        }

        if (!_startedAudio)
        {
            _startedAudio = true;
            _audio.Play();
        }

        _rigidbody.velocity = new Vector2(speed, _rigidbody.velocity.y);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dino_Killing"))
        {
            _animator.SetTrigger("dead");
            _audio.Stop();
            speed = 0;
        }
        else if (collision.CompareTag("Dino_Jump"))
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
        {
            Destroy(collision.gameObject);
            SoundManager.instance.GetEffectSource().PlayOneShot(destroyClip);
        }
    }
}