using System;
using System.Globalization;

public class CalculateSalary : ICalculateSalary
{
    public double CalculateIncrement(string salaryValue, string incrementPercentage)
    {
        // Normalizar el formato de los n�meros, reemplazando comas con puntos
        salaryValue = salaryValue.Replace(',', '.');
        incrementPercentage = incrementPercentage.Replace(',', '.');

        // Intentar convertir salaryValue a un n�mero
        if (!double.TryParse(salaryValue, NumberStyles.Float, CultureInfo.InvariantCulture, out double salary))
        {
            throw new ArgumentException("El valor del salario no es un n�mero v�lido.");
        }

        // Intentar convertir incrementPercentage a un n�mero
        if (!double.TryParse(incrementPercentage, NumberStyles.Float, CultureInfo.InvariantCulture, out double increment))
        {
            throw new ArgumentException("El valor del incremento no es un n�mero v�lido.");
        }

        // Calcular el salario final
        double finalSalary = salary + salary * (increment / 100);
        return finalSalary;
    }
}