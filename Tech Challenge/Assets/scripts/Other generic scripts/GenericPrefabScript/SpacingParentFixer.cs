using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpacingParentFixer : MonoBehaviour
{
    public static SpacingParentFixer Instance;
    private GameObject ListPanel;
    void Awake()
    {
        // Asegurar que solo haya una instancia del editor
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void UpdatePanel()
    {
        RectTransform rect = gameObject.transform.GetComponent<RectTransform>();
        LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
        Canvas.ForceUpdateCanvases();
    }

    public IEnumerator SetSpacing(VerticalLayoutGroup vlg, float spacing, float time)
    {
        yield return new WaitForSeconds(time);
        vlg.spacing = spacing;
    }
}
