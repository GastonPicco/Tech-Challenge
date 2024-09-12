using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using Interface;
using Model;
using ListGeneration;

namespace EmployeeEditBar
{ 
    public class EmployeeModifier
    {
        private string fileName = "EmployeesData.xml";
        private IEmployeeSerializer employeeSerializer;
        private IEmployeeLoader employeeLoader;
        public void ModifyOrCreateEmployeeById(int employeeId, string newFirstName, string newLastName, string newPosition, string newSeniority, string newYearsInCompany)
        {
            List<Employee> employees = LoadEmployeesFromXml(); // Cargar empleados

            // Buscar el empleado por ID
            Employee employee = employees.Find(emp => emp.Id == employeeId);

            if (employee != null)
            {
                // Modificar los datos del empleado existente
                employee.FirstName = newFirstName;
                employee.LastName = newLastName;
                employee.Position = newPosition;
                employee.Seniority = newSeniority;
                employee.YearsInCompany = newYearsInCompany;

                Debug.Log($"Empleado con ID {employeeId} modificado correctamente.");
            }
            else
            {
                // Crear un nuevo empleado con el ID proporcionado
                Employee newEmployee = new Employee(employeeId, newFirstName, newLastName, newPosition, newSeniority, newYearsInCompany);
                employees.Add(newEmployee); // Agregar el nuevo empleado a la lista

                Debug.Log($"Empleado con ID {employeeId} no encontrado, nuevo empleado creado.");
            }

            SaveEmployeesToXml(employees);
        }

        private List<Employee> LoadEmployeesFromXml()
        {
            employeeLoader = new EmployeeLoader();
            return employeeLoader.LoadEmployeesFromXml(fileName);
        }

   
        private void SaveEmployeesToXml(List<Employee> employees)
        {
            employeeSerializer = new EmployeeSerializer();
            employeeSerializer.SaveEmployeesToXml(employees, fileName);
        }
    }

}