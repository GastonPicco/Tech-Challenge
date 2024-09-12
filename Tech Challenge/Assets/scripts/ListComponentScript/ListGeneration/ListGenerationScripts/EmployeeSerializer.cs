using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;
using System;
using Interface;
using Model;

namespace ListGeneration{
    public class EmployeeSerializer : IEmployeeSerializer
    {
        public void SaveEmployeesToXml(List<Employee> employees, string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Employee>));
            string path = Path.Combine(Application.persistentDataPath, fileName);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                serializer.Serialize(stream, employees);
            }

            Debug.Log($"Employees data saved to {path}");
        }
    }
}
