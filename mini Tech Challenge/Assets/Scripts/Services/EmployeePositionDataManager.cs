using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EmployeePositionDataManager : MonoBehaviour 
{
    

    public void BuildAndSavePosition(Position newPosition, List<Seniority> senioritiesToBuild, List<int> employeeCounts)
    {
        string xmlFileName = "PositionsData.xml";
        // cargar los datos existentes antes de generar nuevos datos, si el archivo existe
        List<Position> existingPositions = new List<Position>();
        int totalExistingEmployeeCount = 0;

        FileManager fileManager = new();
        string path = fileManager.GetDocumentsPath(xmlFileName);

        if (File.Exists(path))
        {
            existingPositions = fileManager.LoadPositionsFromXml(xmlFileName);
            totalExistingEmployeeCount = fileManager.GetTotalEmployeeCount(existingPositions);
            Debug.Log("Datos existentes cargados.");
        }
        else
        {
            Debug.Log("No se encontraron datos previos. Generando nuevos datos.");
        }

        // verificar si la posición ya existe
        if (!string.IsNullOrEmpty(newPosition.JobTitle))
        {
            Position existingPosition = existingPositions.Find(pos => pos.JobTitle == newPosition.JobTitle);

            if (existingPosition != null)
            {
                // Si la posición existe, actualizar o agregar los seniorities
                Debug.Log($"Posición {newPosition.JobTitle} ya existe, actualizando seniorities.");

                for (int i = 0; i < senioritiesToBuild.Count; i++)
                {
                    if (string.IsNullOrEmpty(senioritiesToBuild[i].Level)) // verificar si el nivel está vacío o es null
                    {
                        Debug.LogWarning($"El Seniority en la posición {newPosition.JobTitle} está vacío o es nulo. No se guardará.");
                        continue; // Saltar si el seniority está vacío
                    }

                    // buscar si el seniority existe dentro de la posición
                    Seniority existingSeniority = existingPosition.Seniorities.Find(s => s.Level == senioritiesToBuild[i].Level);

                    if (existingSeniority != null)
                    {
                        // seniority existe, actualizar los valores
                        Debug.Log($"Seniority {senioritiesToBuild[i].Level} ya existe, actualizando BaseSalary e IncrementPercentage.");
                        existingSeniority.BaseSalary = senioritiesToBuild[i].BaseSalary;
                        existingSeniority.IncrementPercentage = senioritiesToBuild[i].IncrementPercentage;

                        // agregar empleados
                        AddEmployeesToSeniority(existingSeniority, employeeCounts[i], ref totalExistingEmployeeCount);
                    }
                    else
                    {
                        // Seniority no existe, crear un nuevo seniority con empleados
                        Debug.Log($"Seniority {senioritiesToBuild[i].Level} no existe, creando nuevo seniority.");
                        Seniority newSeniority = CreateNewSeniorityWithEmployees(senioritiesToBuild[i], employeeCounts[i], ref totalExistingEmployeeCount);
                        existingPosition.Seniorities.Add(newSeniority);
                    }
                }
            }
            else
            {
                // Si la posición no existe, crear una nueva posición con los seniorities y empleados
                Debug.Log($"Posición {newPosition.JobTitle} no existe, creando nueva posición.");
                Position createdPosition = CreatePositionWithEmployees(newPosition, senioritiesToBuild, employeeCounts, totalExistingEmployeeCount);
                existingPositions.Add(createdPosition);
            }

            // filtrar y eliminar los seniorities con Level vacío antes de guardar
            foreach (Position position in existingPositions)
            {
                position.Seniorities = position.Seniorities.FindAll(s => !string.IsNullOrEmpty(s.Level));
            }

            // guardar los datos actualizados
            fileManager.SavePositionsToXml(existingPositions, xmlFileName);
        }
        else
        {
            Debug.LogWarning("El JobTitle de la nueva posición está vacío o es nulo. No se guardará esta posición.");
        }
    }

    // Función para agregar empleados a un seniority existente
    public void AddEmployeesToSeniority(Seniority seniority, int employeeCount, ref int currentId)
    {
        for (int j = 0; j < employeeCount; j++)
        {
            seniority.Employees.Add(new Employee(++currentId, $"Name{currentId}", $"LastName{currentId}"));
        }
    }

    // Función para crear un nuevo seniority con empleados
    private Seniority CreateNewSeniorityWithEmployees(Seniority seniorityTemplate, int employeeCount, ref int currentId)
    {
        Seniority newSeniority = new Seniority(seniorityTemplate.Level, seniorityTemplate.BaseSalary, seniorityTemplate.IncrementPercentage, new List<Employee>());

        for (int j = 0; j < employeeCount; j++)
        {
            newSeniority.Employees.Add(new Employee(++currentId, $"Name{currentId}", $"LastName{currentId}"));
        }

        return newSeniority;
    }

    // Función para crear una nueva posición con empleados
    public Position CreatePositionWithEmployees(Position position, List<Seniority> seniorities, List<int> employeeCounts, int startingId)
    {
        Debug.Log("Generando datos para la posición: " + position.JobTitle);

        int currentId = startingId + 1; // La nueva ID empieza después de los empleados existentes

        for (int i = 0; i < seniorities.Count; i++)
        {
            if (string.IsNullOrEmpty(seniorities[i].Level)) // Verificar si el nivel está vacío o es null
            {
                Debug.LogWarning($"El Seniority en la posición {position.JobTitle} está vacío o es nulo. No se guardará.");
                continue; // saltar si el seniority está vacío
            }

            int employeeCount = (i < employeeCounts.Count) ? employeeCounts[i] : 0;

            List<Employee> employees = new List<Employee>();


            for (int j = 0; j < employeeCount; j++)
            {
                employees.Add(new Employee(currentId++, $"Name{(currentId - 1)}", $"LastName{(currentId - 1)}"));
            }

            seniorities[i].Employees = employees;
   
        }

        position.Seniorities = seniorities.FindAll(s => !string.IsNullOrEmpty(s.Level));
        return position;
    }
}
