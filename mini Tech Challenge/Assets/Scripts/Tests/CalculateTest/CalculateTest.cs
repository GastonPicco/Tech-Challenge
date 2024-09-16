using NUnit.Framework;
using System;

public class CalculateTest
{
    private CalculateSalary calculateSalary;

    [SetUp]
    public void Setup()
    {
        // inicializa la clase CalculateSalary antes de cada test
        calculateSalary = new CalculateSalary();
    }

    [Test]
    public void GetSalary_PositiveValues_ReturnsCorrectSalary()
    {
        // prueba que con valores positivos la funcion GetSalary devuelva el salario correcto
        float salary = 1000f;
        float increment = 0.1f; // 10%
        float expectedSalary = 1100f;

        float result = calculateSalary.GetSalary(salary, increment);

        Assert.AreEqual(expectedSalary, result);
    }

    [Test]
    public void GetSalary_ZeroIncrement_ReturnsSameSalary()
    {
        // prueba que si el incremento es 0, el salario devuelto sea el mismo
        float salary = 1000f;
        float increment = 0f;
        float expectedSalary = 1000f;

        float result = calculateSalary.GetSalary(salary, increment);

        Assert.AreEqual(expectedSalary, result);
    }

    [Test]
    public void GetSalary_ZeroSalary_ReturnsZero()
    {
        // prueba que si el salario es 0, el salario final tambien sea 0
        float salary = 0f;
        float increment = 0.1f; // 10%
        float expectedSalary = 0f;

        float result = calculateSalary.GetSalary(salary, increment);

        Assert.AreEqual(expectedSalary, result);
    }

    [Test]
    public void GetSalary_NegativeSalary_ThrowsArgumentException()
    {
        // prueba que si el salario es negativo, lanza una excepcion
        float salary = -1000f;
        float increment = 0.1f;

        var ex = Assert.Throws<ArgumentException>(() => calculateSalary.GetSalary(salary, increment));
        Assert.That(ex.Message, Is.EqualTo("los valores no pueden ser negativos"));
    }

    [Test]
    public void GetSalary_NegativeIncrement_ThrowsArgumentException()
    {
        // prueba que si el incremento es negativo, lanza una excepcion
        float salary = 1000f;
        float increment = -0.1f;

        var ex = Assert.Throws<ArgumentException>(() => calculateSalary.GetSalary(salary, increment));
        Assert.That(ex.Message, Is.EqualTo("los valores no pueden ser negativos"));
    }

    [Test]
    public void GetSalary_BothNegative_ThrowsArgumentException()
    {
        // prueba que si tanto el salario como el incremento son negativos, lanza una excepcion
        float salary = -1000f;
        float increment = -0.1f;

        var ex = Assert.Throws<ArgumentException>(() => calculateSalary.GetSalary(salary, increment));
        Assert.That(ex.Message, Is.EqualTo("los valores no pueden ser negativos"));
    }
}