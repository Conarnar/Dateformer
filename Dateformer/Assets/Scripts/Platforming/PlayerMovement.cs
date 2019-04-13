using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// last edited: Evan Cheng
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jump;
    bool jumping = false;
    float horizontal = 0;
    bool grounded = false;

    Rigidbody2D rb;
    Collider2D col;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        jumping = Input.GetButton("Jump");
        horizontal = Input.GetAxis("Horizontal");
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed * Time.fixedDeltaTime, (jumping && grounded) ? jump : rb.velocity.y);
        grounded = false;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.bounds.max.y < col.bounds.min.y)
        {
            grounded = true;
        }
    }
}
