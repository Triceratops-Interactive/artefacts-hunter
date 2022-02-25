using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class SwitchSpriteOnXYDiff : MonoBehaviour
{
    [SerializeField] private Sprite left;
    [SerializeField] private Sprite right;
    [SerializeField] private Sprite up;
    [SerializeField] private Sprite down;
    [SerializeField] private float SpriteChangeX_threshold = 2.5f;
    [SerializeField] private float SpriteChangeY_threshold = 2.5f;

    private float x_agg = 0;
    private float prev_pos_x = 0;
    private float diff_x = 0;

    private float y_agg = 0;
    private float prev_pos_y = 0;
    private float diff_y = 0;

    Transform mummy_transform;
    private SpriteRenderer mummy_sprite;

    // Start is called before the first frame update
    void Start()
    {
        mummy_transform = gameObject.GetComponent<Transform>();
        mummy_sprite = gameObject.GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(prev_pos_x == 0)
        {
            prev_pos_x = transform.position.x;
        }
        else
        {
            diff_x = transform.position.x- prev_pos_x;
            x_agg = x_agg + diff_x;
        }

        if (prev_pos_y == 0)
        {
            prev_pos_y = transform.position.y;
        }
        else
        {
            diff_y = transform.position.y - prev_pos_y;
            y_agg = y_agg + diff_y;
        }

        //mummy moves right
        if(x_agg>=SpriteChangeX_threshold)
        {
            if (mummy_sprite.sprite != right)
            { 
                mummy_sprite.sprite = right;
            }
            prev_pos_y = 0;
            y_agg = 0;
            prev_pos_x = 0;
            x_agg = 0;
        }
        //mummy move left 
        else if(x_agg <= -SpriteChangeX_threshold)
        {
            if (mummy_sprite.sprite != left)
            {
                mummy_sprite.sprite = left;
            }
            prev_pos_y = 0;
            y_agg = 0;
            prev_pos_x = 0;
            x_agg = 0;
        }

        //mummy moves up
        if (y_agg >= SpriteChangeY_threshold)
        {
            if (mummy_sprite.sprite != up)
            {
                mummy_sprite.sprite = up;
            }
            prev_pos_y = 0;
            y_agg = 0;
            prev_pos_x = 0;
            x_agg = 0;
        }
        //mummy move down 
        else if (y_agg <= -SpriteChangeY_threshold)
        {
            if (mummy_sprite.sprite != down)
            {
                mummy_sprite.sprite = down;
            }

            prev_pos_y = 0;
            y_agg = 0;
            prev_pos_x = 0;
            x_agg = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        // Is this the player?
        PlayerBehaviour check_player = otherCollider.gameObject.GetComponent<PlayerBehaviour>();
        if (check_player != null)
        {
            //Reset player
            SceneManager.LoadScene("pyramid_labyrinth_dark");
        }

    }
}
