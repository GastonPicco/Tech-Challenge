using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetScrollBar : MonoBehaviour
{
    Scrollbar scrollBar;
    private void Start()
    {
        resetScrollBar();
    }
    public void resetScrollBar()
    {
        scrollBar = GetComponent<Scrollbar>();
        scrollBar.value = 1;
    }
}
