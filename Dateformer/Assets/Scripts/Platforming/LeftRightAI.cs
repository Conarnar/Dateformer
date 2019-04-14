using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Moves the GameObject Left then Right as the ground allows
/// lasted edited: Evan Cheng
/// </summary>

[RequireComponent(typeof(Rigidbody2D))]
public class LeftRightAI : MonoBehaviour
{
    public LayerMask groundLayerMask; 
    public Transform groundCheckLeft, groundCheckRight;  
    public float speed;
    bool movingLeft;

    public float pause = 0;

    Rigidbody2D rb;
    Vector2 velocity; 
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        velocity = Vector2.right;  
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        checkGround(); 
        velocity = movingLeft ? Vector2.left * speed : Vector2.right * speed;

        pause -= Time.fixedDeltaTime;

        if (pause < 0)
        {
            rb.MovePosition(rb.position + velocity * -pause);
            pause = 0;
        }
    }

    void checkGround()
    {
        GetComponentInChildren<SpriteRenderer>().flipX = !movingLeft;
        RaycastHit2D leftCheck = Physics2D.Linecast(groundCheckLeft.position, (Vector2)groundCheckLeft.position + Vector2.down, groundLayerMask);
        Debug.DrawLine( groundCheckLeft.position, (Vector2)groundCheckLeft.position + Vector2.down); 
        RaycastHit2D rightCheck = Physics2D.Linecast(groundCheckRight.position, (Vector2)groundCheckRight.position + Vector2.down, groundLayerMask);
        Debug.DrawLine(groundCheckRight.position, (Vector2)groundCheckRight.position + Vector2.down);

        if (leftCheck.collider == null || leftCheck.point == (Vector2)groundCheckLeft.position ||  !leftCheck.collider.CompareTag("Ground"))
        {
            movingLeft = false;
        }

        if (rightCheck.collider == null || rightCheck.point == (Vector2)groundCheckRight.position || !rightCheck.collider.CompareTag("Ground"))
        {
            movingLeft = true; 
        }
    }
}