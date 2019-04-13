using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] Vector2 velocity;
    [SerializeField] float fireDelay;
    [SerializeField] GameObject bullet;

    bool fire = false;
    float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time >= fireDelay)
        {
            fire = true;
            time -= fireDelay;
        }
    }

    void FixedUpdate()
    {
        if (fire)
        {
            Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

            if (pos.x > -0.1f && pos.x < 1.1f && pos.y > -0.1f && pos.y < 1.1f)
            {
                GameObject obj = Instantiate(bullet, transform.position, Quaternion.identity);
                obj.GetComponent<Rigidbody2D>().velocity = velocity;
            }
            
            fire = false;
        }
    }
}
