using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed;
    Rigidbody2D rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Limit max speed
        if (Mathf.Abs(rigidBody.velocity.x) > maxSpeed)
        {
            rigidBody.velocity = new Vector2(maxSpeed * rigidBody.velocity.normalized.x, rigidBody.velocity.y);
        }
        // move
        else
        {
            float h = Input.GetAxisRaw("Horizontal");

            rigidBody.AddForce(Vector2.right * h, ForceMode2D.Impulse);
        }

        // Stop when button up
        if (Input.GetButtonUp("Horizontal"))
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.normalized.x * 0.5f, rigidBody.velocity.y);
        }
    }

}
