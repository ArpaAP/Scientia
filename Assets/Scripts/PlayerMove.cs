using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed;
    public float jumpPower;
    public Sprite[] sprites;
    Rigidbody2D rigidBody;
    SpriteRenderer spriteRenderer;
    Animator animator;
    bool isMoving;
    bool justJumped;
    bool isJumping;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        isMoving = false;
        justJumped = false;
        isJumping = false;
    }

    void ResetSprite(int index)
    {
        spriteRenderer.sprite = sprites[index];
    }

    void MoveHorizontalUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float direction = h == 0 ? rigidBody.velocity.normalized.x : h;

        this.isMoving = false;

        // Stop when both arrow key down
        if (h == 0)
        {
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
            ResetSprite(1);
        }
        else
        {
            // Limit max speed
            if (Mathf.Abs(rigidBody.velocity.x) > maxSpeed)
            {
                rigidBody.velocity = new Vector2(maxSpeed * direction, rigidBody.velocity.y);
                this.isMoving = true;
            }
            // move
            else
            {
                this.isMoving = true;
                rigidBody.AddForce(Vector2.right * h * 2f, ForceMode2D.Impulse);
            }
        }

        // Stop when button up
        if (Input.GetButtonUp("Horizontal"))
        {
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
            this.isMoving = false;
            ResetSprite(1);
        }
    }

    void JumpUpdate()
    {
        if (!isJumping && Input.GetButtonDown("Jump"))
        {
            justJumped = true;
            isJumping = true;
            ResetSprite(1);
        }
    }

    void JumpFixedUpdate()
    {
        if (justJumped)
        {
            justJumped = false;
            rigidBody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }

        // if (rigidBody.velocity.y < 0)
        // {
        //     Debug.DrawRay(rigidBody.position, Vector3.down * 20, new Color(0, 1, 0));
        //     RaycastHit2D raycastHit = Physics2D.Raycast(rigidBody.position, Vector3.down, 20, LayerMask.GetMask("Platform"));

        //     if (raycastHit.collider != null)
        //     {

        //         if (raycastHit.distance < 10f)
        //         {
        //             isJumping = false;
        //             ResetSprite(1);
        //         }
        //     }
        // }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Platform"))
        {
            isJumping = false;
            ResetSprite(1);
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

    void AnimationUpdate()
    {
        if (isMoving && !isJumping)
        {
            animator.enabled = true;
        }
        else
        {
            animator.enabled = false;
        }
    }

    void Update()
    {
        FlipUpdate();
        JumpUpdate();
        AnimationUpdate();
    }

    void FixedUpdate()
    {
        MoveHorizontalUpdate();
        JumpFixedUpdate();
    }
}
