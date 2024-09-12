using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CalculateSalaryTest
{

    private CalculateSalary salaryCalculator;

    [SetUp]
    public void SetUp()
    {
        salaryCalculator = new CalculateSalary();
    }

    [Test]
    public void CalculateIncrement_ShouldReturnCorrectValue_ForBaseCase()
    {
        string salary = "1000";
        string increment = "10";
        double expected = 1100;

        double result = salaryCalculator.CalculateIncrement(salary, increment);

        Debug.Log($"Expected: {expected}, Received: {result}");

        Assert.AreEqual(expected, result);
    }

    [Test]
    public void CalculateIncrement_ShouldReturnSameValue_WhenIncrementIsZero()
    {
        string salary = "1000";
        string increment = "0";
        double expected = 1000;

        double result = salaryCalculator.CalculateIncrement(salary, increment);

        Debug.Log($"Expected: {expected}, Received: {result}");

        Assert.AreEqual(expected, result);
    }

    [Test]
    public void CalculateIncrement_ShouldReturnCorrectValue_ForNegativeIncrement()
    {

        string salary = "1000";
        string increment = "-10";
        double expected = 900;

        double result = salaryCalculator.CalculateIncrement(salary, increment);

        Debug.Log($"Expected: {expected}, Received: {result}");

        Assert.AreEqual(expected, result);
    }

    [Test]
    public void CalculateIncrement_ShouldReturnCorrectValue_ForSalaryWithDecimals()
    {
        string salary = "1500.50";
        string increment = "12.5";
        double expected = 1688.06;

        double result = salaryCalculator.CalculateIncrement(salary, increment);

        Debug.Log($"Expected: {expected}, Received: {result}");

        Assert.AreEqual(expected, result, 0.01); 
    }

    [Test]
    public void CalculateIncrement_ShouldReturnDoubleSalary_WhenIncrementIs100Percent()
    {
        string salary = "1000";
        string increment = "100";
        double expected = 2000;

        double result = salaryCalculator.CalculateIncrement(salary, increment);

        Debug.Log($"Expected: {expected}, Received: {result}");

        Assert.AreEqual(expected, result);
    }

    [Test]
    public void CalculateIncrement_ShouldReturnZero_WhenSalaryIsZero()
    {
        string salary = "0";
        string increment = "15";
        double expected = 0;

        double result = salaryCalculator.CalculateIncrement(salary, increment);

        Debug.Log($"Expected: {expected}, Received: {result}");

        Assert.AreEqual(expected, result);
    }

    [Test]
    public void CalculateIncrement_ShouldHandleDecimalStrings_ForBothSalaryAndIncrement()
    {
        string salary = "1234.56";
        string increment = "7.89";
        double expected = 1331.96;

        double result = salaryCalculator.CalculateIncrement(salary, increment);

        Debug.Log($"Expected: {expected}, Received: {result}");

        Assert.AreEqual(expected, result, 0.01);
    }

    [Test]
    public void CalculateIncrement_ShouldThrowException_ForInvalidIncrementString()
    {
        string salary = "1234.56";
        string increment = "Hello World";

        var ex = Assert.Throws<ArgumentException>(() =>
        {
            salaryCalculator.CalculateIncrement(salary, increment);
        });

        Assert.AreEqual("El valor del incremento no es un número válido.", ex.Message);
    }
    [Test]
    public void CalculateIncrement_ShouldThrowException_ForInvalidSalaryString()
    {
        string salary = "Duck";
        string increment = "5";

        var ex = Assert.Throws<ArgumentException>(() =>
        {
            salaryCalculator.CalculateIncrement(salary, increment);
        });

        Assert.AreEqual("El valor del salario no es un número válido.", ex.Message);
    }
}
