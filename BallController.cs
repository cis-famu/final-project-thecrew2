using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 5.0f; // Speed of the ball
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        LaunchBall();
    }

    void LaunchBall()
    {
        // Launch the ball in a random direction
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(0, 2) == 0 ? -1 : 1;
        rb.velocity = new Vector2(speed * x, speed * y);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // Add more complex interaction based on what the ball hits
        if (col.gameObject.name == "Paddle")
        {
            // You can adjust the reflection based on where it hits the paddle, etc.
            Vector2 adjustAngle = new Vector2(Random.Range(0.5f, 1f), Random.Range(0.5f, 1f));
            rb.velocity = adjustAngle * speed;
        }
    }

    // Optionally add a method to reset the ball when a point is scored
    public void ResetBall()
    {
        rb.velocity = Vector2.zero; // Stop the ball
        transform.position = Vector2.zero; // Center the ball
        LaunchBall(); // Relaunch the ball
    }

    // Function to handle the bouncing behavior
    public void Bounce(Vector3 wallNormal)
    {
        // Calculate the bounce direction using the wall's normal vector
        Vector3 bounceDirection = Vector3.Reflect(transform.forward, wallNormal);

        // Apply the bounce direction to the ball's velocity
        GetComponent<Rigidbody>().velocity = bounceDirection * speed;
    }

    public int lastPlayerTouched; // 1 for player 1, 2 for player 2

    // ... Your existing ball control code ...

    private void OnCollisionEnter2D1(Collision2D collision)
    {
        // Check the tag or name of the collision object to determine which paddle hit the ball
        if (collision.collider.CompareTag("Player1Paddle"))
        {
            lastPlayerTouched = 1;
        }
        else if (collision.collider.CompareTag("Player2Paddle"))
        {
            lastPlayerTouched = 2;
        }

        // ... Other collision handling code ...
    }

}
