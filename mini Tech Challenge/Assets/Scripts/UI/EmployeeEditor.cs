using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class EmployeeEditor : MonoBehaviour
{
    [SerializeField] TMP_InputField idInput, nameInput, lastNameInput, positionInput, seniorityInput;
    [SerializeField] GameObject List;
    string xmlFileName = "PositionsData.xml";
    FileManager fileManager;

    private void Start()
    {
        fileManager = new FileManager();
    }

    public void SetValues(string id, string name, string lastname, string position, string seniority)
    {
        idInput.text = id;
        nameInput.text = name;
        lastNameInput.text = lastname;
        positionInput.text = position;
        seniorityInput.text = seniority;
    }

    public void EditEmployee()
    {
 
        if (!int.TryParse(idInput.text, out int employeeId))
        {
            Debug.LogError($"El ID {idInput.text} no es un número entero válido.");
            return; 
        }

       
        List<Position> positions = fileManager.LoadPositionsFromXml(xmlFileName);

    
        string newName = nameInput.text;
        string newLastName = lastNameInput.text;
        string newPosition = positionInput.text;
        string newSeniority = seniorityInput.text;

        // Buscar empleado por ID
        Employee employee = null;
        Position currentPosition = null;
        Seniority currentSeniority = null;

        foreach (var position in positions)
        {
            foreach (var seniority in position.Seniorities)
            {
                employee = seniority.Employees.Find(e => e.Id == employeeId);
                if (employee != null)
                {
                    currentPosition = position;
                    currentSeniority = seniority;
                    break;
                }
            }
            if (employee != null) break;
        }

        // si el empleado no existe verificar si la posicion y el seniority existen para crear uno nuevo
        if (employee == null)
        {
            Debug.LogWarning($"El empleado con ID {employeeId} no existe. Verificando si se puede crear uno nuevo.");

            //verificar si la posicion existe
            Position newPositionObj = positions.Find(p => p.JobTitle == newPosition);
            if (newPositionObj == null)
            {
                Debug.LogError($"La posición {newPosition} no existe.");
                return;
            }

            // verificar si el seniority existe en la posición
            Seniority newSeniorityObj = newPositionObj.Seniorities.Find(s => s.Level == newSeniority);
            if (newSeniorityObj == null)
            {
                Debug.LogError($"El seniority {newSeniority} no existe en la posición {newPosition}.");
                return;
            }

            // crear un nuevo empleado en la posicion y seniority correctos
            Employee newEmployee = new Employee(employeeId, newName, newLastName);
            newSeniorityObj.Employees.Add(newEmployee);
            Debug.Log($"Nuevo empleado creado en la posición {newPosition} y seniority {newSeniority}.");

            // guardar los datos actualizados en el archivo XML
            fileManager.SavePositionsToXml(positions, xmlFileName);
            Debug.Log("Datos guardados correctamente.");
            return;
        }

        // si el empleado existe verificar si la posicion coincide
        if (currentPosition.JobTitle != newPosition)
        {
            Position newPositionObj = positions.Find(p => p.JobTitle == newPosition);

            if (newPositionObj == null)
            {
                Debug.LogError($"La posición {newPosition} no existe.");
                return;
            }

            // verificar sii el seniority coincide
            Seniority newSeniorityObj = newPositionObj.Seniorities.Find(s => s.Level == newSeniority);

            if (newSeniorityObj == null)
            {
                Debug.LogError($"El seniority {newSeniority} no existe en la posición {newPosition}.");
                return;
            }

            // mover empleado a la nueva posicion y seniority
            currentSeniority.Employees.Remove(employee);
            newSeniorityObj.Employees.Add(employee);
            Debug.Log($"Empleado movido a la posición {newPosition} y seniority {newSeniority}.");
        }
        else
        {
            // verificar siel seniority coincide
            if (currentSeniority.Level != newSeniority)
            {
                Seniority newSeniorityObj = currentPosition.Seniorities.Find(s => s.Level == newSeniority);

                if (newSeniorityObj == null)
                {
                    Debug.LogError($"El seniority {newSeniority} no existe en la posición {newPosition}.");
                    return;
                }

                // mover empleado al nuevo seniority
                currentSeniority.Employees.Remove(employee);
                newSeniorityObj.Employees.Add(employee);
                Debug.Log($"Empleado movido al nuevo seniority {newSeniority} dentro de la misma posición {newPosition}.");
            }
        }

        // actualizar la informacion del empleado
        employee.FirstName = newName;
        employee.LastName = newLastName;

        // guardar los datos actualizados en el archivo xml
        fileManager.SavePositionsToXml(positions, xmlFileName);
        Debug.Log("Empleado editado correctamente.");
    }

    public void DeleteEmployeeById(int employeeId)
    {
        // cargar datos del archivo cml
        List<Position> positions = fileManager.LoadPositionsFromXml(xmlFileName);

        Employee employee = null;
        Seniority currentSeniority = null;

        //buscar empleado por ID
        foreach (var position in positions)
        {
            foreach (var seniority in position.Seniorities)
            {
                employee = seniority.Employees.Find(e => e.Id == employeeId);
                if (employee != null)
                {
                    currentSeniority = seniority;
                    break;
                }
            }
            if (employee != null) break; // termina el foreach por que ya encontro un employee
        }

        // si el empleado no existe, lanzar error
        if (employee == null)
        {
            Debug.LogError($"El empleado con ID {employeeId} no existe. No se puede eliminar.");
            return;
        }

        // eliminar el empleado del seniority
        currentSeniority.Employees.Remove(employee);
        Debug.Log($"Empleado con ID {employeeId} eliminado correctamente.");

        // guardar los datos actualizados en el archivo xml
        fileManager.SavePositionsToXml(positions, xmlFileName);
    }

    public void Clear()
    {
        idInput.text = string.Empty;
        nameInput.text = string.Empty;
        lastNameInput.text = string.Empty;
        positionInput.text = string.Empty;
        seniorityInput.text = string.Empty;
    }
}
