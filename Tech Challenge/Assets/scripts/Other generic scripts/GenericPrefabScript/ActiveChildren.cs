using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveChildren : MonoBehaviour
{
    public GameObject children;

    public void ClickToggleActiveChildren()
    {
        if (children.activeInHierarchy) 
        { 
            children.SetActive(false); 
        }

        else 
        { 
            children.SetActive(true); 
        }
    }
}
