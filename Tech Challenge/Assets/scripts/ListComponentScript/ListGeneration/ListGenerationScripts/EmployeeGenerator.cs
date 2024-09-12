using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;
using Interface;
using System;


namespace ListGeneration
{
    public class EmployeeGenerator : IEmployeeGenerator
    {
        private readonly string[] firstNames =
        {
    "John", "Jane", "Michael", "Emily", "David", "Laura", "Chris", "Sarah", "Daniel", "Jessica",
    "Matthew", "Emma", "Joshua", "Olivia", "Andrew", "Sophia", "James", "Mia", "William", "Ava",
    "Ryan", "Isabella", "Alexander", "Madison", "Nicholas", "Chloe", "Anthony", "Lily", "Benjamin", "Grace"
    };

        private readonly string[] lastNames =
    {
    "Smith", "Johnson", "Williams", "Brown", "Jones",
    "Garcia", "Miller", "Davis", "Rodriguez", "Martinez",
    "Hernandez", "Lopez", "Gonzalez", "Wilson", "Anderson",
    "Thomas", "Taylor", "Moore", "Jackson", "Martin"
};

        public List<Employee> GenerateEmployees(List<string[]> employeeData)
        {
            List<Employee> employees = new List<Employee>();
            int idCounter = 1;

            foreach (var data in employeeData)
            {
                // employeeData es un string[] con los siguientes campos:
                // data[0] = Position, data[1] = Seniority, data[2] = BaseSalary, data[3] = Increase, data[4] = Count

                string position = data[0];   // Cargo o posición (ej. "HR", "Engineering")
                string seniority = data[1];  // Nivel de seniority (ej. "Senior", "Junior")
                if (!int.TryParse(data[4], out int numEmployees) || numEmployees < 0)
                {
                    throw new ArgumentException("La cantidad de empleados debe ser un número entero no negativo.");
                }

                // Crear los empleados según la cantidad indicada en 'count'
                employees.AddRange(CreateEmployees(ref idCounter, position, seniority, numEmployees));
            }
            return employees;


        }
        private List<Employee> CreateEmployees(ref int idCounter, string position, string seniority, int count)
        {
            List<Employee> employeeList = new List<Employee>();

            for (int i = 0; i < count; i++)
            {
                string randomFirstName = firstNames[UnityEngine.Random.Range(0, firstNames.Length)];
                string randomLastName = lastNames[UnityEngine.Random.Range(0, lastNames.Length)];

                employeeList.Add(new Employee(
                       idCounter++,
                       randomFirstName,
                       randomLastName,
                       position,
                       seniority,
                       UnityEngine.Random.Range(1, 15).ToString() // Años entre 1 y 15 (((No se USA)))
                ));
            }
            return employeeList;
        }

    }
}


