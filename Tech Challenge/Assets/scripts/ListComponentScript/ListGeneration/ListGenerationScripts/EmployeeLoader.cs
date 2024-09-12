using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Interface;
using Model;
using UnityEngine;

namespace ListGeneration
{
    public class EmployeeLoader : IEmployeeLoader
    {
        public List<Employee> LoadEmployeesFromXml(string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Employee>));
            string path = Path.Combine(Application.persistentDataPath, fileName);

            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                return (List<Employee>)serializer.Deserialize(stream);
            }
        }
    }

}
