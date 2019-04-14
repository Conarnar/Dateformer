using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderBouncesPlayer : MonoBehaviour
{
    public float bounceVelocity;
    [SerializeField] LeftRightAI ai;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(rb.velocity.x, bounceVelocity);
            ai.pause = 0.2f;
        }
    }
}