using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Moves the GameObject Left then Right as the ground allows
/// lasted edited: Evan Cheng
/// </summary>

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class LeftRightAI : MonoBehaviour
{
    public Transform groundCheckLeft, groundCheckRight;  
    public float speed;
    bool movingLeft;

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
        rb.MovePosition(rb.position + velocity * Time.deltaTime);
    }

    void checkGround()
    {
        RaycastHit2D leftCheck = Physics2D.Linecast(groundCheckLeft.position, (Vector2)groundCheckLeft.position + Vector2.down);
        Debug.DrawLine( groundCheckLeft.position, (Vector2)groundCheckLeft.position + Vector2.down); 
        RaycastHit2D rightCheck = Physics2D.Linecast(groundCheckRight.position, (Vector2)groundCheckRight.position + Vector2.down);
        Debug.DrawLine(groundCheckRight.position, (Vector2)groundCheckRight.position + Vector2.down);

        Debug.Log(leftCheck.collider + "    " + rightCheck.collider); 

        if (leftCheck.collider == null || !leftCheck.collider.CompareTag("Ground"))
        {
            movingLeft = false;
            Debug.Log("triggered");
        }

        if (rightCheck.collider == null ||  !rightCheck.collider.CompareTag("Ground"))
        {
            movingLeft = true; 
        }
    }
}