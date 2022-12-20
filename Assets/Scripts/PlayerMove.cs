using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed;
    public float maxJumpSpeed;
    public Sprite[] sprites;
    Rigidbody2D rigidBody;
    SpriteRenderer spriteRenderer;
    Animator animator;
    bool isJumping;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        isJumping = false;
    }

    void ResetSprite()
    {
        spriteRenderer.sprite = sprites[1];
    }

    void MoveHorizontalUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float direction = h == 0 ? rigidBody.velocity.normalized.x : h;

        animator.enabled = false;

        // Stop when both arrow key down
        if (h == 0)
        {
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
            ResetSprite();
        }
        else
        {
            // Limit max speed
            if (Mathf.Abs(rigidBody.velocity.x) > maxSpeed)
            {
                rigidBody.velocity = new Vector2(maxSpeed * direction, rigidBody.velocity.y);
                animator.enabled = true;
            }
            // move
            else
            {
                animator.enabled = true;
                rigidBody.AddForce(Vector2.right * h * 2f, ForceMode2D.Impulse);
            }
        }

        // Stop when button up
        if (Input.GetButtonUp("Horizontal"))
        {
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
            animator.enabled = false;
            ResetSprite();
        }
    }

    void FlipUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float direction = h == 0 ? rigidBody.velocity.normalized.x : h;

        // Flip
        if (h != 0)
        {
            spriteRenderer.flipX = direction == -1;
        }
    }

    void Update()
    {
        FlipUpdate();
    }


    void FixedUpdate()
    {
        MoveHorizontalUpdate();

        if (isJumping && Input.GetAxisRaw("Jump") == 1)
        {
            isJumping = true;
            rigidBody.AddForce(Vector2.up * 2, ForceMode2D.Impulse);

        }
    }
}
