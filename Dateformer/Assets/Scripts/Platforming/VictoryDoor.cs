using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class VictoryDoor : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("_Victory_Screen"); 
        }
    }

    private void Start()
    {
        GameManager manager = GameManager.singleton; 
        if(!(manager.spikeAffinity.hasBeenClosed && manager.bulletAffinity.hasBeenClosed && manager.enemyAffinity.hasBeenClosed))
        {
            Destroy(gameObject); 
        }
    }
}
