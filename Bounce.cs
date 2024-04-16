using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc : MonoBehaviour
{
    public float speed = 10f; // Adjust the speed as necessary
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        LaunchBallAtStart();
    }

    private void LaunchBallAtStart()
    {
        // Launch the ball in a random direction
        float x = Random.Range(0, 2) * 2 - 1; // This will be -1 or 1
        float y = Random.Range(0, 2) * 2 - 1;
        rb.velocity = new Vector2(speed * x, speed * y).normalized * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Paddle"))
        {
            // Calculate a new velocity vector based on where the ball hit the paddle
            Vector2 hitPoint = collision.contacts[0].point;
            Vector2 paddleCenter = new Vector2(collision.collider.bounds.center.x, collision.collider.bounds.center.y);

            float difference = hitPoint.y - paddleCenter.y;

            Vector2 newVelocity = new Vector2(rb.velocity.x, difference).normalized * speed;
            rb.velocity = newVelocity;
        }
    }
}
