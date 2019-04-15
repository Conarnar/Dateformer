using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().flipY = GetComponent<Rigidbody2D>().velocity.x < 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

        if (pos.x < -0.1f || pos.x > 1.1f || pos.y < -0.1f || pos.y > 1.1f)
        {
            Destroy(gameObject);
        }
    }
}
