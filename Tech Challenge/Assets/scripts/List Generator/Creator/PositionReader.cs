using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using Interface;
using Model;
using ListGeneration;

namespace Creator
{
    public class PositionReader : MonoBehaviour
    {
        public string fileNamePosition = "PositionsData.xml";
        private string fileNameEmployees = "EmployeesData.xml";
        private IEmployeeGenerator employeeGenerator;
        private IEmployeeSerializer employeeSerializer;


        // Hacer que la lista de posiciones sea visible en el Inspector
        [SerializeField] public List<Position> positions = new List<Position>();


        // Método para deserializar las posiciones desde el archivo XML
        private List<Position> LoadPositionsFromXml(string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Position>));
            string path = Path.Combine(Application.persistentDataPath, fileName);

            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                return (List<Position>)serializer.Deserialize(stream);
            }
        }
        public void LoadPositions()
        {
            employeeGenerator = new EmployeeGenerator();
            employeeSerializer = new EmployeeSerializer();
            List<string[]> positionData = new List<string[]>();
            positions = LoadPositionsFromXml(fileNamePosition);

            // Iterar sobre las posiciones
            foreach (Position position in positions)
            {
                //Debug.Log($"Posición: {position.PositionName} !");

                // Asegúrate de que todas las listas tengan el mismo tamaño
                int dataCount = Math.Min(Math.Min(position.Seniorities.Count, position.BaseSalaries.Count),
                                         Math.Min(position.Increases.Count, position.Counts.Count));

                for (int i = 0; i < dataCount; i++)
                {
                    // Leer y procesar los datos del mismo índice
                    string seniority = position.Seniorities[i];
                    string baseSalary = position.BaseSalaries[i];
                    string increase = position.Increases[i];
                    string count = position.Counts[i];

                    //Debug.Log($" En la posicion {position.PositionName}  Seniority: {seniority}, Base Salary: {baseSalary}, Increase: {increase}, Count: {count} Para le indice {i}");
                    string[] seniorityInPositionData = new string[] { position.PositionName , seniority , baseSalary , increase , count , i.ToString()};
                    positionData.Add(seniorityInPositionData);
                }
                List<Employee> employees = employeeGenerator.GenerateEmployees(positionData);
                employeeSerializer.SaveEmployeesToXml(employees, fileNameEmployees);
            }

        }
    }
}
