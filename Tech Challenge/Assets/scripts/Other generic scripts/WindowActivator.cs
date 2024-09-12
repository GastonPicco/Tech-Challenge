using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowActivator : MonoBehaviour
{
    public void CloseWindow()
    {
        gameObject.SetActive(false);
    }
    public void OpenWindow()
    {
        gameObject.SetActive(true);
    }
}
