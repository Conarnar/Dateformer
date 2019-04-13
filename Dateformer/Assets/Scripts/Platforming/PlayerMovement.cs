using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jump;
    [SerializeField] float groundCheck;

    bool jumping = false;
    float horizontal = 0;
    bool grounded {
        get
        {
            return Physics2D.Raycast(transform.position, Vector2.down, groundCheck, 1 << 8);
        }
    }

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
    }
}
