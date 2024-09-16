using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SpacingFixer : MonoBehaviour
{
    public GameObject childList;
    //Por Algun motivo que no entiendo la lista se renderiza mejor con este fix, parece que con solo instanciar el componente no actualiza sus medidas
    public void ListToggle()
    {
        if (childList.activeInHierarchy)
        {
            childList.SetActive(false);
        }
        else
        {
            childList.SetActive(true);
        }
    }

    public void FixPadding()
    {

        RectTransform rect = childList.transform.GetComponent<RectTransform>();
        LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
        rect = gameObject.transform.GetComponent<RectTransform>();
        LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
        Canvas.ForceUpdateCanvases();

        GameObject parent = GameObject.Find("ListContent");

        rect = parent.GetComponent<RectTransform>();
        LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
        Canvas.ForceUpdateCanvases();

    }


}