using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    public float moveSpeed = 5f;
    public float initMoveSpeed;

    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public Animator animator;
    Vector2 movement;

    private void Awake()
    {
        instance = this;
        initMoveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        // Movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        if (movement.x > 0)
            sr.flipX = false;
        else
            sr.flipX = true;
    }
}
