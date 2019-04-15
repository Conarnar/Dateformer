using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] float velocity;
    [SerializeField] float fireDelay;
    [SerializeField] GameObject bullet;

    Transform player;

    SpriteRenderer renderer;

    bool fire = false;
    float time = 0;

    float angleToPlayer = 0;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        time = -fireDelay - Random.Range(0, fireDelay);
    }

    // Update is called once per frame
    void Update()
    {
        angleToPlayer = Mathf.Atan2(player.position.y - transform.position.y, player.position.x - transform.position.x);
        transform.rotation = Quaternion.Euler(0, 0, angleToPlayer * Mathf.Rad2Deg);

        renderer.flipY = Mathf.Cos(angleToPlayer) < 0;

        if ((transform.position - player.position).sqrMagnitude > 16)
        {
            time += Time.deltaTime;

            if (time >= fireDelay)
            {
                fire = true;
                time -= fireDelay;
            }
        }
    }

    void FixedUpdate()
    {
        if (fire)
        {
            Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

            if (pos.x > -0.1f && pos.x < 1.1f && pos.y > -0.1f && pos.y < 1.1f)
            {
                GameObject obj = Instantiate(bullet, transform.position + new Vector3(Mathf.Cos(angleToPlayer), Mathf.Sin(angleToPlayer)), transform.rotation);
                obj.GetComponent<Rigidbody2D>().velocity = (player.position - transform.position).normalized * velocity;
            }
            
            fire = false;
        }
    }
}
