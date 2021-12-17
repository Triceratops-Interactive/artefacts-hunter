using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dino : MonoBehaviour
{
    // 1 - Designer variables

    /// <summary>
    /// Object speed
    /// </summary>
    public Vector2 speed = new Vector2(2, 2);

    /// <summary>
    /// Moving direction
    /// </summary>
    public Vector2 direction = new Vector2(1, 0);

    private Vector2 movement;
    private Rigidbody2D rigidbodyComponent;

    void Update()
    {
        // 2 - Movement
        movement = new Vector2(
          speed.x * direction.x,
          speed.y * direction.y);
    }

    void FixedUpdate()
    {
        if (rigidbodyComponent == null) rigidbodyComponent = GetComponent<Rigidbody2D>();

        // Apply movement to the rigidbody
        rigidbodyComponent.velocity = movement;
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Obstacle"))
        {
            col.gameObject.SetActive(false);
        }


    }
}
