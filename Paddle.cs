using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float speed = 10f;
    public KeyCode moveUp = KeyCode.W;
    public KeyCode moveDown = KeyCode.S;
    public KeyCode moveRight = KeyCode.D;     // Key for moving right
    public KeyCode moveLeft = KeyCode.A;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 movement = rb.velocity;

        if (Input.GetKey(moveUp))
        {
            movement.y = speed;
        }
        else if (Input.GetKey(moveDown))
        {
            movement.y = -speed;
        }
        else
        {
            movement.y = 0;
        }

        if (Input.GetKey(moveRight))
        {
            movement.x = speed;
        }
        else if (Input.GetKey(moveLeft))
        {
            movement.x = -speed;
        }
        else
        {
            movement.x = 0;
        }

        // Apply the movement to the paddle's velocity
        rb.velocity = movement;
    }
}
