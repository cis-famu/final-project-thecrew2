using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBounce : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
        // Check if the ball collided with an object tagged as "Wall"
        if (collision.gameObject.tag == "Wall")
        {
            // The physics material on the ball's collider and the wall's collider will handle the bounce.
            // No additional code is needed here unless you want to add extra gameplay logic.
        }
    }

}
