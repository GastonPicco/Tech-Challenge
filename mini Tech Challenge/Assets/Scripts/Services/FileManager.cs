using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class FileManager : IFileManager
{
    public string GetDocumentsPath(string fileName)
    {
        string documentsPath = Application.persistentDataPath;
        return Path.Combine(documentsPath, fileName = "PositionsData.xml");
    }

    public void SavePositionsToXml(List<Position> positions, string fileName)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Position>));
        string path = GetDocumentsPath(fileName);

        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, positions);
        }
        Debug.Log($"Datos guardados en: {path}");
    }

    public List<Position> LoadPositionsFromXml(string fileName)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Position>));
        string path = GetDocumentsPath(fileName);

        using (FileStream stream = new FileStream(path, FileMode.Open))
        {
            return (List<Position>)serializer.Deserialize(stream);
        }
    }

    public int GetTotalEmployeeCount(List<Position> positions)
    {
        int employeeCount = 0;
        foreach (Position position in positions)
        {
            foreach (Seniority seniority in position.Seniorities)
            {
                employeeCount += seniority.Employees.Count;
            }
        }
        return employeeCount;
    }
    public void DeleteFile(string fileName)
    {
        string path = GetDocumentsPath(fileName);

        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log($"Archivo {fileName} borrado exitosamente.");
        }
        else
        {
            Debug.LogWarning($"El archivo {fileName} no existe.");
        }
    }
}