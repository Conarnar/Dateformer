using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Vector2 bottomLeftBounds;
    [SerializeField] Vector2 topRightBounds;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(player.position.x, bottomLeftBounds.x, topRightBounds.x), Mathf.Clamp(player.position.y, bottomLeftBounds.y, topRightBounds.y), transform.position.z);
    }
}
