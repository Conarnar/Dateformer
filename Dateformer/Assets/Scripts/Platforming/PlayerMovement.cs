using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// last edited: Evan Cheng
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] LayerMask groundLayer; 
    [SerializeField] float speed;
    [SerializeField] float jump;
    [SerializeField] float jumpHoldDuration;
    [SerializeField] SpriteRenderer spriteRen;
    Animator anim;
    
    bool jumping = false;
    bool jumpHolding = false;
    float jumpHeld = 0;
    float horizontal = 0;

    bool grounded {
        get
        {
            float minX = col.bounds.min.x;
            float maxX = col.bounds.max.x;
            float minY = col.bounds.min.y;
            
            for (int i = 0; i < 5; i++)
            {
                Debug.DrawLine(new Vector2(Mathf.Lerp(minX, maxX, i / 4f), minY), new Vector2(Mathf.Lerp(minX, maxX, i / 4f), minY - 0.1f), Color.red);
                if (Physics2D.Raycast(new Vector2(Mathf.Lerp(minX, maxX, i / 4f), minY), Vector2.down, 0.1f, 1 << 8))
                {
                    return true;
                }
            }

            return false;
        }
    }

    Rigidbody2D rb;
    Collider2D col;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        spriteRen = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        jumping = jumping || Input.GetButtonDown("Jump");
        jumpHolding = jumpHeld > 0 && Input.GetButton("Jump");
        horizontal = Input.GetAxis("Horizontal");
    }

    void FixedUpdate()
    {
        if (jumping && grounded)
        {
            jumpHeld += Time.fixedDeltaTime;
        }
        else {
            if (jumpHolding && jumpHeld < jumpHoldDuration)
            {
                jumpHeld += Time.fixedDeltaTime;
                jumping = true;
            }
            else
            {
                jumping = false;
                jumpHeld = grounded ? 0 : jumpHoldDuration;
            }
        }
        anim.SetBool("isGrounded", grounded);
        anim.SetFloat("horizontal", Mathf.Abs(horizontal));
        if(horizontal != 0)
            spriteRen.flipX = (horizontal < 0);
        rb.velocity = new Vector2(horizontal * speed * Time.fixedDeltaTime, jumping ? jump : rb.velocity.y);
        anim.SetFloat("vertical", rb.velocity.y);
        jumping = false;
    }
}
