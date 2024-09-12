using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;
using UnityEngine.UIElements;
using Model;
using EmployeeEditBar;

namespace ListComponent
{
    public class EmployeeUI : MonoBehaviour
    {
        private ICalculateSalary calculateSalary;
        public TMP_Text idText;
        public TMP_Text nameText;
        public TMP_Text positionText;
        public TMP_Text seniorityText;
        public TMP_Text yearsText;
        public TMP_Text baseSalaryText;
        public TMP_Text finalSalaryText;

        public GameObject employeeEditorPrefab; // El prefab del editor de UI
        public Transform parentTransform; // Donde se instanciará el editor en la UI
        private GameObject currentEditorUI;


        public void SetEmployeeData(Employee employee, List<Model.Position> positions)
        {
            idText.text = $"id: {employee.Id.ToString()}";
            nameText.text = $"{employee.FirstName} {employee.LastName}";
            positionText.text = employee.Position;
            seniorityText.text = employee.Seniority.ToString();
            yearsText.text = $"{employee.YearsInCompany.ToString()} years";

            UpdateEmployeeData(employee, positions);
        }

        public void UpdateEmployeeData(Employee employee, List<Model.Position> positions)
        {
            if (string.IsNullOrEmpty(employee.FirstName) && string.IsNullOrEmpty(employee.LastName))
            {
                nameText.text = "";
                positionText.text = "";
                seniorityText.text = "";
                gameObject.SetActive(false);
                return;
            }

            foreach (Model.Position position in positions)
            {
                // Compara la posición del empleado con la posición en la lista
                if (employee.Position == position.PositionName)
                {
                    // todas las listas tengan el mismo tamaño
                    int dataCount = Math.Min(position.Seniorities.Count, Math.Min(position.BaseSalaries.Count, position.Increases.Count));

                    for (int i = 0; i < dataCount; i++)
                    {
                        // Si se encuentra la coincidencia del seniority
                        if (employee.Seniority == position.Seniorities[i])
                        {
                            calculateSalary = new CalculateSalary();

                            string baseSalary = position.BaseSalaries[i];
                            string increment = position.Increases[i];
                            double finalSalary = calculateSalary.CalculateIncrement(baseSalary, increment);

                            // Actualiza los datos de salario
                            CompleteSalaryData(baseSalary, increment, finalSalary);
                            return;
                        }
                    }
                }
            }

            // Si no encuentra datos de salario para la posición o seniority, mostrar valores por defecto
            CompleteSalaryData("", "", 0);
        }
        public void DeleteLine()
        {
            var id = idText.text.Split(" ");
            EditorValueManager.Instance.OnEditOrCreateEmployee(id[1], "","","","","");
            gameObject.SetActive(false);

        }

        public void CompleteSalaryData( string baseSalary , string increment , double finalSalary)
        {
            //Debug.Log("los valores son " + baseSalary + finalSalary + increment);
            if( baseSalary == "" || increment == "" )
            {
                baseSalaryText.text = $" {positionText.text} don´t have a base salary value";
                finalSalaryText.text = $" {positionText.text} don´t have an increment '%'";
                baseSalaryText.fontSize = 11.5f;
                finalSalaryText.fontSize = 11.5f;
                baseSalaryText.color = Color.red;
                finalSalaryText.color = Color.red;
            }
            else
            {
                baseSalaryText.text = $"Base Salary: {baseSalary}$";
                finalSalaryText.text = $"Final Salary: {finalSalary}$";
                baseSalaryText.fontSize = 12;
                finalSalaryText.fontSize = 12;
                baseSalaryText.color = Color.black;
                finalSalaryText.color = Color.black;
            }
        }
        public void OnEditButton()
        {
            if (EditorValueManager.Instance)
            {
                EditorValueManager.Instance.SetValues(idText.text,nameText.text,positionText.text,seniorityText.text, yearsText.text);
            }
        }
    }

}

