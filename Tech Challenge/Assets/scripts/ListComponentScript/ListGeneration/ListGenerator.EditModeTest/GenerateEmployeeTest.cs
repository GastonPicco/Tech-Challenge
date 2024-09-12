using System.Collections;
using System.Collections.Generic;
using ListGeneration;
using NUnit.Framework;
using UnityEngine;
using Model;
using System.IO;
using System;

public class GenerateEmployeeTest
{
    private EmployeeGenerator employeeGenerator;

    // Método Setup que se ejecuta antes de cada test
    [SetUp]
    public void Setup()
    {
        employeeGenerator = new EmployeeGenerator(); // Inicializar el generador
    }


    [Test]
    public void GenerateEmployees_CorrectCountPerSeniority()
    {
       
        List<string[]> employeeData = new List<string[]>
        {
            new string[] { "Engineering", "Junior", "1500", "5", "3" },       // 3 Juniors
            new string[] { "Engineering", "Semi Senior", "3000", "7", "2" },  // 2 Semi Seniors
            new string[] { "Engineering", "Senior", "5000", "10", "1" }       // 1 Senior
        };

        
        Debug.Log("Datos enviados para la generación de empleados:");
        foreach (var data in employeeData)
        {
            Debug.Log($"Posición: {data[0]}, Seniority: {data[1]}, Salario: {data[2]}, Aumento: {data[3]}, Cantidad: {data[4]}");
        }

        // Act: Generar la lista de empleados
        List<Employee> employees = employeeGenerator.GenerateEmployees(employeeData);

        
        int juniorCount = 0;
        int semiSeniorCount = 0;
        int seniorCount = 0;

        foreach (var employee in employees)
        {
            if (employee.Seniority == "Junior") juniorCount++;
            else if (employee.Seniority == "Semi Senior") semiSeniorCount++;
            else if (employee.Seniority == "Senior") seniorCount++;
        }

        // Imprimir resultados obtenidos
        Debug.Log($"Conteo de empleados generado: Junior = {juniorCount}, Semi Senior = {semiSeniorCount}, Senior = {seniorCount}");

        // Assert: 
        Assert.AreEqual(expected: 3, actual: juniorCount,
            $"La cantidad de Juniors es incorrecta. Esperado: 3, Encontrado: {juniorCount}");
        Assert.AreEqual(expected: 2, actual: semiSeniorCount,
            $"La cantidad de Semi Seniors es incorrecta. Esperado: 2, Encontrado: {semiSeniorCount}");
        Assert.AreEqual(expected: 1, actual: seniorCount,
            $"La cantidad de Seniors es incorrecta. Esperado: 1, Encontrado: {seniorCount}");
    }
    [Test]
    public void GenerateEmployees_CorrectCountPerSeniorityInEachPosition()
    {
        // Arrange: Definir los datos de los empleados
        List<string[]> employeeData = new List<string[]>
    {
        new string[] { "HR", "Junior", "500", "0.5", "13" },
        new string[] { "HR", "Semi Senior", "1000", "2", "2" },
        new string[] { "HR", "Senior", "1500", "5", "5" },
        new string[] { "Engineering", "Junior", "1500", "5", "32" },
        new string[] { "Engineering", "Semi Senior", "3000", "7", "68" },
        new string[] { "Engineering", "Senior", "5000", "10", "50" },
        new string[] { "Artist", "Semi Senior", "1200", "2.5", "20" },
        new string[] { "Artist", "Senior", "2000", "5", "5" },
        new string[] { "Design", "Junior", "800", "4", "15" },
        new string[] { "Design", "Senior", "2000", "7", "10" },
        new string[] { "PMs", "Semi Senior", "2400", "5", "20" },
        new string[] { "PMs", "Senior", "4000", "10", "10" },
        new string[] { "Ceo", "Ceo", "20000", "100", "1" }
    };

        // Act: Generar la lista de empleados
        List<Employee> employees = employeeGenerator.GenerateEmployees(employeeData);

        // Contar la cantidad de empleados por posición y nivel de seniority
        var actualCounts = new Dictionary<(string position, string seniority), int>();
        foreach (var employee in employees)
        {
            var key = (employee.Position, employee.Seniority);
            if (actualCounts.ContainsKey(key))
            {
                actualCounts[key]++;
            }
            else
            {
                actualCounts[key] = 1;
            }
        }

        // Definir los conteos esperados
        var expectedCounts = new Dictionary<(string position, string seniority), int>
    {
        { ("HR", "Junior"), 13 },
        { ("HR", "Semi Senior"), 2 },
        { ("HR", "Senior"), 5 },
        { ("Engineering", "Junior"), 32 },
        { ("Engineering", "Semi Senior"), 68 },
        { ("Engineering", "Senior"), 50 },
        { ("Artist", "Semi Senior"), 20 },
        { ("Artist", "Senior"), 5 },
        { ("Design", "Junior"), 15 },
        { ("Design", "Senior"), 10 },
        { ("PMs", "Semi Senior"), 20 },
        { ("PMs", "Senior"), 10 },
        { ("Ceo", "Ceo"), 1 }
    };

        // Assert: 
        foreach (var expected in expectedCounts)
        {
            var position = expected.Key.position;
            var seniority = expected.Key.seniority;
            var expectedCount = expected.Value;

            if (actualCounts.ContainsKey((position, seniority)))
            {
                Assert.AreEqual(expectedCount, actualCounts[(position, seniority)],
                    $"Error: En la posición '{position}' y seniority '{seniority}', " +
                    $"se esperaba {expectedCount} empleados, pero se encontraron {actualCounts[(position, seniority)]}.");
            }
            else
            {
                Assert.Fail($"Error: No se encontraron empleados con la posición '{position}' y seniority '{seniority}'.");
            }
        }

        // Imprimir resultados para depuración
        Debug.Log("Conteo actual de empleados:");
        foreach (var actual in actualCounts)
        {
            Debug.Log($"Posición: {actual.Key.position}, Seniority: {actual.Key.seniority}, Cantidad: {actual.Value}");
        }

        Debug.Log("Conteo esperado de empleados:");
        foreach (var expected in expectedCounts)
        {
            Debug.Log($"Posición: {expected.Key.position}, Seniority: {expected.Key.seniority}, Cantidad esperada: {expected.Value}");
        }

    }
    [Test]
    public void GenerateEmployees_ThrowsException_WithInvalidNegativeData()
    {
        // Arrange: 
        List<string[]> employeeData = new List<string[]>
            {
                new string[] { "Engineering", "Junior", "1500", "5", "-4" }, // Invalid: Cantidad negativa
                new string[] { "Engineering", "Semi Senior", "3000", "7", "2" }, // Valid
                new string[] { "Engineering", "Senior", "5000", "10", "1" } // Valid
            };

        // Act & Assert: 
        Assert.Throws<ArgumentException>(code: () => { List<Employee> employees = employeeGenerator.GenerateEmployees(employeeData); });
    }
    [Test]
    public void GenerateEmployees_LargeNumberOfEmployees()
    {
        // Arrange: 
        List<string[]> largeEmployeeData = new List<string[]>
    {
        new string[] { "Engineering", "Junior", "1500", "5", "1000" } // 1000 Juniors
    };

        // Act:
        List<Employee> employees = employeeGenerator.GenerateEmployees(largeEmployeeData);

        // Assert: 
        Assert.AreEqual(1000, employees.Count, "La cantidad de empleados generados no es correcta.");
    }
    [Test]
    public void GenerateEmployees_ThrowsException_WithCommaInCount()
    {
            // Arrange:
        List<string[]> employeeData = new List<string[]>
        {
            new string[] { "Engineering", "Senior", "1500", "5", "3,14" } // Cantidad con coma
        };
        Assert.Throws<ArgumentException>(() => employeeGenerator.GenerateEmployees(employeeData),
        "Se esperaba una ArgumentException debido a una coma en la cantidad, pero no se lanzó ninguna.");
       
    }
    [Test]
    public void GenerateEmployees_ThrowsException_WithWord()
    {
        // Arrange: 
        List<string[]> employeeData = new List<string[]>
        {
            new string[] { "HR", "Junior", "1500", "5", "Senior" } 
        };
        Assert.Throws<ArgumentException>(() => employeeGenerator.GenerateEmployees(employeeData),
        "Se esperaba una ArgumentException debido a una coma en la cantidad, pero no se lanzó ninguna.");

    }
    [Test]
    public void GenerateEmployees_ThrowsException_withSimbol()
    {
        // Arrange: 
        List<string[]> employeeData = new List<string[]>
        {
            new string[] { "Ceo", "Ceo", "1500", "5", "@" } 
        };
        Assert.Throws<ArgumentException>(() => employeeGenerator.GenerateEmployees(employeeData),
        "Se esperaba una ArgumentException debido a una coma en la cantidad, pero no se lanzó ninguna.");

    }

}



