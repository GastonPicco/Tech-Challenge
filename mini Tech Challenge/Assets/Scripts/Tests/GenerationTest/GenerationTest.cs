using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class GenerationTest
{
    private EmployeePositionDataManager employeeManager;

    [SetUp]
    public void Setup()
    {
        // Inicializamos el manager antes de cada prueba
        employeeManager = new EmployeePositionDataManager();
    }

    [Test]
    public void BuildAndSavePosition_EmptySeniorityLevel_SkipsSaving()
    {
        // Arrange: Creamos una posición con un seniority vacío
        Position newPosition = new Position("Designer", new List<Seniority>());
        List<Seniority> seniorities = new List<Seniority>
        {
            new Seniority("", 1200f, 0.05f, new List<Employee>())
        };
        List<int> employeeCounts = new List<int> { 1 };

        // Act: Ejecutamos el método
        employeeManager.BuildAndSavePosition(newPosition, seniorities, employeeCounts);

        // Log en consola
        Debug.Log($"Esperado: 0 Seniorities guardados, Resultado: {newPosition.Seniorities.Count}");

        // Assert: Verificamos que el Seniority vacío no se haya guardado
        Assert.AreEqual(0, newPosition.Seniorities.Count);
    }

    [Test]
    public void CreatePositionWithEmployees_ValidInput_CreatesEmployeesCorrectly()
    {
        // Arrange: Creamos una nueva posicion con seniorities y empleados
        Position newPosition = new Position("Manager", new List<Seniority>());
        List<Seniority> seniorities = new List<Seniority>
        {
            new Seniority("Senior", 3000f, 0.2f, new List<Employee>())
        };
        List<int> employeeCounts = new List<int> { 20 }; // Crear 2 empleados

        // Act: Creamos la posición con empleados
        Position createdPosition = employeeManager.CreatePositionWithEmployees(newPosition, seniorities, employeeCounts, 0);

        // Valores esperados
        int expectedEmployeeCount = 20;
        string expectedFirstEmployeeName = "Name1";

        // Log en consola
        Debug.Log($"Esperado: {expectedEmployeeCount} empleados, Resultado: {createdPosition.Seniorities[0].Employees.Count}");
        Debug.Log($"Esperado: {expectedFirstEmployeeName}, Resultado: {createdPosition.Seniorities[0].Employees[0].FirstName}");

        // Assert: Verificamos que los empleados fueron creados
        Assert.AreEqual(expectedEmployeeCount, createdPosition.Seniorities[0].Employees.Count);
        Assert.AreEqual(expectedFirstEmployeeName, createdPosition.Seniorities[0].Employees[0].FirstName);
    }
    [Test]
    public void CreatePositionWithEmployees_ValidInput_CreatesEmployeesCountNegative()
    {
        // Arrange: Creamos una nueva posicion con seniorities y empleados
        Position newPosition = new Position("Manager", new List<Seniority>());
        List<Seniority> seniorities = new List<Seniority>
        {
            new Seniority("Senior", 3000f, 0.2f, new List<Employee>())
        };
        List<int> employeeCounts = new List<int> { -20 }; 

        // Act: Creamos la posición con empleados negativos
        Position createdPosition = employeeManager.CreatePositionWithEmployees(newPosition, seniorities, employeeCounts, 0);

        // Valores esperados
        int expectedEmployeeCount = 0;

        Debug.Log($"Esperado: {expectedEmployeeCount} empleados, Resultado: {createdPosition.Seniorities[0].Employees.Count}");

        // Assert:
        Assert.AreEqual(expectedEmployeeCount, createdPosition.Seniorities[0].Employees.Count);
    }
    [Test]
    public void CreatePositionWithEmployees_ValidInput_CreatesEmployeesCountZero()
    {
        // Arrange: Creamos una nueva posicion con seniorities y empleados
        Position newPosition = new Position("Manager", new List<Seniority>());
        List<Seniority> seniorities = new List<Seniority>
        {
            new Seniority("Senior", 3000f, 0.2f, new List<Employee>())
        };
        List<int> employeeCounts = new List<int> { 0 }; 

        // Act: Creamos la posición con empleados negativos
        Position createdPosition = employeeManager.CreatePositionWithEmployees(newPosition, seniorities, employeeCounts, 0);

        // Valores esperados
        int expectedEmployeeCount = 0;
        string positionExpected = "Manager";

        Debug.Log($"Esperado: {expectedEmployeeCount} empleados, Resultado: {createdPosition.Seniorities[0].Employees.Count}");

        // Assert:
        Assert.AreEqual(expectedEmployeeCount, createdPosition.Seniorities[0].Employees.Count);
        Assert.AreEqual(positionExpected, createdPosition.JobTitle);
    }

    [Test]
    public void AddEmployeesToSeniority_AddsCorrectNumberOfEmployees()
    {
        // Arrange: Creamos un seniority y lo inicializamos con una lista de empleados vacía
        Seniority seniority = new Seniority("Junior", 1000f, 0.05f, new List<Employee>());
        int initialId = 0;

        // Act: Añadimos 3 empleados al seniority
        employeeManager.AddEmployeesToSeniority(seniority, 3, ref initialId);

        // Valores esperados
        int expectedEmployeeCount = 3;
        string expectedFirstEmployeeName = "Name1";

        // Log en consola
        Debug.Log($"Esperado: {expectedEmployeeCount} empleados, Resultado: {seniority.Employees.Count}");
        Debug.Log($"Esperado: {expectedFirstEmployeeName}, Resultado: {seniority.Employees[0].FirstName}");

        // Assert: Verificamos que se crearon 3 empleados
        Assert.AreEqual(expectedEmployeeCount, seniority.Employees.Count);
        Assert.AreEqual(expectedFirstEmployeeName, seniority.Employees[0].FirstName);
    }
}
