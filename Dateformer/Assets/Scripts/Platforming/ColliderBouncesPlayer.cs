using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderBouncesPlayer : MonoBehaviour
{
    public float bounceVelocity; 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("collided");
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(rb.velocity.x, bounceVelocity);  
        }
    }
    
}