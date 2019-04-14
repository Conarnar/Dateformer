using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sets desired child active while setting all others inactive
/// </summary>
public class ChildrenToggle : MonoBehaviour
{
    public void toggleChild(int index)
    {
        disableAllChildren();
        transform.GetChild(index).gameObject.SetActive(true); 
    }

    void disableAllChildren()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
