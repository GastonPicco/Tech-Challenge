using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class ListPrefabGenerator : MonoBehaviour
{
    [SerializeField] Transform ListTransform;
    [SerializeField] GameObject PositionPrefab, SeniorityPrefab, ListPrefab;
    string xmlfileName;

    public void StartLoad()
    {
        xmlfileName = "PositionsData.xml";
        StartCoroutine(LoadPrefabsCoroutine());
    }

    IEnumerator LoadPrefabsCoroutine()
    {
        ClearListTransform();
        FileManager fileManager = new FileManager();
        List<Position> positionsToLoad = fileManager.LoadPositionsFromXml(xmlfileName);

        // iterar sobre cada posición, pero con una pausa entre cada frame
        foreach (Position position in positionsToLoad)
        {
            yield return StartCoroutine(LoadSinglePosition(position));  // pausar después de cada posición
        }

        ResetUI(ListTransform.gameObject);
    }

    IEnumerator LoadSinglePosition(Position position)
    {
        string positionTitle = position.JobTitle;
        GameObject newPositionPrefab = Instantiate(PositionPrefab, ListTransform);

        Transform positionButton = newPositionPrefab.transform.GetChild(0).transform;
        Transform positionButtonText = positionButton.GetChild(0);
        Transform positionButtonText2 = positionButton.GetChild(1);
        TMP_Text positionText = positionButtonText.GetComponent<TMP_Text>();
        TMP_Text positionText2 = positionButtonText2.GetComponent<TMP_Text>();
        positionText.text = positionTitle;

        // Contar total de empleados en la posición
        int totalEmployees = 0;
        foreach (Seniority seniority in position.Seniorities)
        {
            totalEmployees += seniority.Employees.Count;
        }
        positionText2.text = totalEmployees.ToString(); // Mostrar la cantidad de empleados en la posición

        Transform positionContent = newPositionPrefab.transform.GetChild(1).transform;

        newPositionPrefab.name = positionTitle;
        Canvas.ForceUpdateCanvases();

        foreach (Seniority seniority in position.Seniorities)
        {
            string seniorityTitle = seniority.Level;
            GameObject newSeniorityPrefab = Instantiate(SeniorityPrefab, positionContent);

            Transform seniorityButton = newSeniorityPrefab.transform.GetChild(0).transform;
            Transform seniorityButtonText = seniorityButton.GetChild(0);
            Transform seniorityButtonText2 = seniorityButton.GetChild(1);
            TMP_Text seniorityText = seniorityButtonText.GetComponent<TMP_Text>();
            TMP_Text seniorityText2 = seniorityButtonText2.GetComponent<TMP_Text>();
            seniorityText.text = seniorityTitle;

            // Contar total de empleados en el seniority
            int seniorityEmployeesCount = seniority.Employees.Count;
            seniorityText2.text = seniorityEmployeesCount.ToString(); // Mostrar la cantidad de empleados en el seniority

            Transform seniorityContent = newSeniorityPrefab.transform.GetChild(1).transform;

            newSeniorityPrefab.name = seniorityTitle;
            Canvas.ForceUpdateCanvases();

            foreach (Employee employee in seniority.Employees)
            {
                GameObject newEmployeePrefab = Instantiate(ListPrefab, seniorityContent);
                ListElement listElement = newEmployeePrefab.GetComponent<ListElement>();
                listElement.id = Convert.ToString(employee.Id);
                listElement.employeeName = employee.FirstName;
                listElement.lastname = employee.LastName;
                listElement.position = position.JobTitle;
                listElement.seniority = seniority.Level;
                listElement.salary = seniority.BaseSalary;
                listElement.increment = seniority.IncrementPercentage;
                Canvas.ForceUpdateCanvases();
            }
        }

        yield return null; // pausa la ejecución para el siguiente frame antes de cargar la siguiente posición
    }

    public void ResetUI(GameObject prefab)
    {
        foreach (Transform child in prefab.GetComponentsInChildren<Transform>())
        {
            RectTransform rect = child.GetComponent<RectTransform>();
            LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
            Canvas.ForceUpdateCanvases();
        }
    }

    public void ClearListTransform()
    {
        // Recorre cada hijo dentro de ListTransform y los destruye
        foreach (Transform child in ListTransform)
        {
            Destroy(child.gameObject);
        }

        // Actualiza el canvas después de limpiar
        Canvas.ForceUpdateCanvases();
    }
}
