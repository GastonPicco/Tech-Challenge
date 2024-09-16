using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ListCreatorInputSistemManager : MonoBehaviour
{
    [SerializeField] TMP_InputField inputFieldsposition;
    [SerializeField] TMP_InputField[] inputFieldsSeniority, inputFieldsSalary, inputFieldsIncrement, inputFieldsCount;
    [SerializeField] bool isPreset;
    [SerializeField] string position;
    [SerializeField] string[] seniority, salary, increment, count;
    private void Start()
    {
        if (isPreset) {
            inputFieldsposition.text = position;
            for (int i = 0; i < seniority.Length; i++) { inputFieldsSeniority[i].text = seniority[i]; }
            for (int i = 0; i < salary.Length; i++) { inputFieldsSalary[i].text = salary[i]; }
            for (int i = 0; i < increment.Length; i++) { inputFieldsIncrement[i].text = increment[i]; }
            for (int i = 0; i < count.Length; i++) { inputFieldsCount[i].text = count[i]; }
        }
    }
    public void ManageInputs()
    {
        try
        {
            if (!string.IsNullOrEmpty(inputFieldsposition.text))
            {
         
                ValidateSeniorityUniqueness();

                Position newPosition = new Position(inputFieldsposition.text, new List<Seniority>());

                (List<Seniority> seniorities, List<int> counts) = GetSeniorityData();

                EmployeePositionDataManager creator = new EmployeePositionDataManager();

                creator.BuildAndSavePosition(newPosition, seniorities, counts);
            }
            else
            {
                Debug.Log($"{this.gameObject.name} no tiene ninguna posición asignada");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }
    }

    public (List<Seniority>, List<int>) GetSeniorityData()
    {
        List<Seniority> seniorities = new List<Seniority>();
        List<int> counts = new List<int>();

        for (int i = 0; i < inputFieldsSeniority.Length; i++)
        {
            if (inputFieldsSeniority[i] != null)
            {
                float salary;
                float increment;
                int count;

                salary = !string.IsNullOrEmpty(inputFieldsSalary[i].text) ? float.Parse(inputFieldsSalary[i].text) : 0;
                increment = !string.IsNullOrEmpty(inputFieldsIncrement[i].text) ? float.Parse(inputFieldsIncrement[i].text) : 0;
                count = !string.IsNullOrEmpty(inputFieldsCount[i].text) ? Convert.ToInt32(inputFieldsCount[i].text) : 0;

                Seniority seniority = new Seniority(inputFieldsSeniority[i].text, salary, increment, new List<Employee>());
                seniorities.Add(seniority);
                counts.Add(count);
            }
        }

        return (seniorities, counts);
    }

  
    private void ValidateSeniorityUniqueness()
    {
        HashSet<string> uniqueSeniorities = new HashSet<string>();

        for (int i = 0; i < inputFieldsSeniority.Length; i++)
        {
            string seniority = inputFieldsSeniority[i].text;

            if (string.IsNullOrEmpty(seniority))
            {
                continue;
            }
            //Los HashSet no permiten duplicados, por lo que si al intentar agregar un valor el método Add devuelve false, significa que ese valor ya existe y lanza una excepción.
            // Si el seniority ya está en el conjunto, lanzamos una excepción
            if (!uniqueSeniorities.Add(seniority))
            {
                throw new Exception($"El Seniority '{seniority}' está duplicado. Los Seniorities deben ser únicos.");
            }
        }
    }
    public void ClearInputs()
    {
        inputFieldsposition.text = string.Empty;
        foreach(var input in inputFieldsSeniority)
        {
            input.text = string.Empty;
        }
        foreach(var input in inputFieldsSalary)
        {
            input.text = string.Empty;
        }
        foreach (var input in inputFieldsIncrement)
        {
            input.text = string.Empty;
        }
        foreach (var input in inputFieldsCount)
        {
            input.text = string.Empty;
        }

    }
}
