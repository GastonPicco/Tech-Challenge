using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

namespace EmployeeEditBar
{
    public class EditorValueManager: MonoBehaviour
    {
        private EmployeeModifier employeeModifier;
        public GameObject idField, nameField, lastNameField, positionField, seniorityField, yearsField;
        public static EditorValueManager Instance;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void Start()
        {
            employeeModifier = new EmployeeModifier();
        }

        public void OnEditOrCreateEmployee()
        {

            int employeeId;
            bool isIdValid = Int32.TryParse(idField.GetComponent<TMP_InputField>().text, out employeeId);

            if (!isIdValid)
            {
                Debug.LogError("El ID ingresado no es válido.");
                return;
            }
            string newFirstName = nameField.GetComponent<TMP_InputField>().text;
            string newLastName = lastNameField.GetComponent<TMP_InputField>().text;
            string newPosition = positionField.GetComponent<TMP_InputField>().text;
            string newSeniority = seniorityField.GetComponent<TMP_InputField>().text;
            string newYearsInCompany = yearsField.GetComponent<TMP_InputField>().text;


            // Modificar o crear el empleado
            employeeModifier.ModifyOrCreateEmployeeById(employeeId, newFirstName, newLastName, newPosition, newSeniority, newYearsInCompany);

            SetValues();
        }
        public void OnEditOrCreateEmployee(string id, string nameField, string lastNameField , string positionField , string seniorityField, string yearsField)
        {

            int employeeId;
            bool isIdValid = Int32.TryParse(id, out employeeId);

            if (!isIdValid)
            {
                Debug.LogError("El ID ingresado no es válido.");
                return;
            }
            string newFirstName = nameField;
            string newLastName = lastNameField;
            string newPosition = positionField;
            string newSeniority = seniorityField;
            string newYearsInCompany = yearsField;


           // Modificar o crear el empleado
            employeeModifier.ModifyOrCreateEmployeeById(employeeId, newFirstName, newLastName, newPosition, newSeniority, newYearsInCompany);

            SetValues();
        }
        public void SetValues()
        {
            idField.GetComponent<TMP_InputField>().text = string.Empty;
            nameField.GetComponent<TMP_InputField>().text = string.Empty;
            lastNameField.GetComponent<TMP_InputField>().text = string.Empty;
            positionField.GetComponent<TMP_InputField>().text = string.Empty;
            seniorityField.GetComponent<TMP_InputField>().text = string.Empty;
            yearsField.GetComponent<TMP_InputField>().text = string.Empty;
        }
        public void SetValues(string id, string name, string position, string seniority, string Years)
        {
            string[] idSplit = id.Split(' ');
            idField.GetComponent<TMP_InputField>().text = idSplit[1];
            string[] nameSpliter = name.Split(' ');
            nameField.GetComponent<TMP_InputField>().text = nameSpliter[0];
            lastNameField.GetComponent<TMP_InputField>().text = nameSpliter[1];
            positionField.GetComponent<TMP_InputField>().text = position;
            seniorityField.GetComponent<TMP_InputField>().text = seniority;
            string[] yearsSpliter = Years.Split(' ');
            yearsField.GetComponent<TMP_InputField>().text = yearsSpliter[0];
        }
    }

}