using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionCreator : MonoBehaviour
{
    [SerializeField] GameObject[] PositionManager;
    public void SendData()
    {
        foreach (var position in PositionManager)
        {
            ListCreatorInputSistemManager inputManager = position.GetComponent<ListCreatorInputSistemManager>();
            inputManager.ManageInputs();
        }
    }
    public void OverWriteData()
    {
        FileManager fileManager = new FileManager();
        fileManager.DeleteFile("PositionsData.xml");
        foreach (var position in PositionManager)
        {
            ListCreatorInputSistemManager inputManager = position.GetComponent<ListCreatorInputSistemManager>();
            inputManager.ManageInputs();
        }
    }
}
