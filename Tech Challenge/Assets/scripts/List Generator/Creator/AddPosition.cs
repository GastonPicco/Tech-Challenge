using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPosition : MonoBehaviour
{
    [SerializeField] GameObject[] newPosition;
    [SerializeField] int maxNewPositions = 4;
    [SerializeField] int index = 0;

    public void CreateNewPosition()
    {
        newPosition[index].SetActive(true);
        index += 1;
        if (index >= maxNewPositions)
        {
            gameObject.SetActive(false);
        }
    }
}
