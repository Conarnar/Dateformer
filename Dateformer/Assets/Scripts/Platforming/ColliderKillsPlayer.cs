using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Kills the player when the player touches the collider. 
/// last edited: Evan Cheng (4/13/19)
/// </summary>
public class ColliderKillsPlayer : MonoBehaviour
{
    public string enemyName;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player died to " + enemyName);
            GameManager.singleton.TransitionEvent(enemyName);
            collision.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.singleton.TransitionEvent(enemyName);
            collision.gameObject.SetActive(false);
        }
    }
}
