using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugUtility : MonoBehaviour
{
    public float timeScale;

    private void Update()
    {
        Time.timeScale = timeScale; 
    }
}
