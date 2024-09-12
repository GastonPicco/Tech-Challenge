using System;
using System.Globalization;

public class CalculateSalary : ICalculateSalary
{
    public double CalculateIncrement(string salaryValue, string incrementPercentage)
    {
        // Normalizar el formato de los números, reemplazando comas con puntos
        salaryValue = salaryValue.Replace(',', '.');
        incrementPercentage = incrementPercentage.Replace(',', '.');

        // Intentar convertir salaryValue a un número
        if (!double.TryParse(salaryValue, NumberStyles.Float, CultureInfo.InvariantCulture, out double salary))
        {
            throw new ArgumentException("El valor del salario no es un número válido.");
        }

        // Intentar convertir incrementPercentage a un número
        if (!double.TryParse(incrementPercentage, NumberStyles.Float, CultureInfo.InvariantCulture, out double increment))
        {
            throw new ArgumentException("El valor del incremento no es un número válido.");
        }

        // Calcular el salario final
        double finalSalary = salary + salary * (increment / 100);
        return finalSalary;
    }
}