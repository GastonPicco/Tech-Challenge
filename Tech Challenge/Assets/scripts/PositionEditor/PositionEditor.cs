using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Model;
using System.Xml.Serialization;
using System.IO;
using Interface;
using ListGeneration;
using System;

public class PositionEditor : MonoBehaviour
{
    [SerializeField] TMP_InputField positionInput, seniorityInput, baseSalaryInput, incrementSalaryInput;
    string fileName = "PositionsData.xml";
    private IPositionLoader positionLoader;
    private List<Model.Position> positions = new List<Model.Position>();


    public void EditOrCreatePositionValues()
    {
        positionLoader = new PositionLoader();
        positions = positionLoader.LoadPositionFromXml(fileName);

        // Validar los inputs de salario y aumento antes de continuar
        double baseSalary = ValidateAndParseDouble(baseSalaryInput.text, "Base Salary");
        double incrementSalary = ValidateAndParseDouble(incrementSalaryInput.text, "Increment Salary");


        // Buscar si la posición ya existe
        Position position = positions.Find(pos => pos.PositionName == positionInput.text);
        if (position != null)
        {
            // Modificar posición si existe
            Debug.Log($"Editando posición existente: {position.PositionName}");
            bool seniorityExists = false;

            // Iterar por los seniorities para verificar si ya existe el que se está ingresando
            for (int i = 0; i < position.Seniorities.Count; i++)
            {
                if (position.Seniorities[i] == seniorityInput.text)
                {
                    // Seniority ya existe, actualizamos los datos
                    position.Seniorities[i] = seniorityInput.text;
                    position.BaseSalaries[i] = baseSalaryInput.text;
                    position.Increases[i] = incrementSalaryInput.text;
                    seniorityExists = true;
                    Debug.Log($"Seniority {seniorityInput.text} actualizado en la posición {position.PositionName}.");
                    break;
                }
            }

            // Si el seniority no existe, lo creamos
            if (!seniorityExists)
            {
                position.Seniorities.Add(seniorityInput.text);
                position.BaseSalaries.Add(baseSalaryInput.text);
                position.Increases.Add(incrementSalaryInput.text);
                Debug.Log($"Nuevo Seniority {seniorityInput.text} creado en la posición {position.PositionName}.");
            }
        }
        else
        {
            // Crear nueva posición si no existe
            Debug.Log($"Creando nueva posición: {positionInput.text}");
            Position newPosition = new Position
            {
                PositionName = positionInput.text,
                Seniorities = new List<string> { seniorityInput.text },
                BaseSalaries = new List<string> { baseSalaryInput.text },
                Increases = new List<string> { incrementSalaryInput.text }
            };

            positions.Add(newPosition);
            Debug.Log($"Nueva posición {positionInput.text} creada con el Seniority {seniorityInput.text}.");
        }

        // Guardar cambios en el archivo XML
        SavePositionsToXml(positions);
    }

    private void SavePositionsToXml(List<Model.Position> positions)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Model.Position>));
        string path = Path.Combine(Application.persistentDataPath, fileName);

        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, positions);
        }
        Debug.Log("Datos de posiciones guardados correctamente en XML.");
    }
    private double ValidateAndParseDouble(string input, string fieldName)
    {
        if (!double.TryParse(input, out double value))
        {
            throw new ArgumentException($"El valor ingresado en {fieldName} no es un número válido.");
        }
        return value;
    }
}