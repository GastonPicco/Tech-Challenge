using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ListElement : MonoBehaviour
{
    public string id, employeeName, lastname, position, seniority;
    [SerializeField]private TMP_Text idText, fullNameText, salaryText, incrementText, finalSalaryText;
    public float salary, increment;
    private void Start()
    {
        CalculateSalary calculateSalary = new CalculateSalary();
        float finalSalary = calculateSalary.GetSalary(salary, increment);

        idText.text = $"id: {id}";
        fullNameText.text = $"{employeeName} {lastname}";
        salaryText.text = "Base salary: " + Convert.ToString(salary) + " U$D";
        incrementText.text = increment * 100 + "%";
        finalSalaryText.text = "Final salary: " + finalSalary + " U$D";
    }
    public void OnEditButtonClicked()
    {
        GameObject Editor = GameObject.Find("EmployeeEditor");
        EmployeeEditor employeeEditor = Editor.GetComponent<EmployeeEditor>();
        employeeEditor.SetValues(id, employeeName, lastname, position, seniority);
    }
    public void OnDeleteButtonClicked() 
    {
        GameObject Editor = GameObject.Find("EmployeeEditor");
        EmployeeEditor employeeEditor = Editor.GetComponent<EmployeeEditor>();
        employeeEditor.DeleteEmployeeById(int.Parse(id));
        GameObject app = GameObject.Find("App");
        ListPrefabGenerator listPrefabGenerator = app.GetComponent<ListPrefabGenerator>();

        gameObject.SetActive(false);
        FixPadding();
    }
    public void FixPadding()
    {
        GameObject parent = GameObject.Find("ListContent");

        RectTransform rect = parent.GetComponent<RectTransform>();
        LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
        Canvas.ForceUpdateCanvases();

    }

}
