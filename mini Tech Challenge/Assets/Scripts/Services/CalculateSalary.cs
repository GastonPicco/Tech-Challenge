using System;

public class CalculateSalary 
{
    // metodo para obtener el salario, validando que los valores no sean negativos
    public float GetSalary(float salary, float increment)
    {
        if (salary < 0 || increment < 0)
        {
            throw new ArgumentException("los valores no pueden ser negativos");
        }
        return salary + (salary * increment);
    }
}