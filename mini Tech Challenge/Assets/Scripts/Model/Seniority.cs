using System;
using System.Collections.Generic;

public class Seniority
{
    public string Level { get; set; } 
    public float BaseSalary { get; set; } 
    public float IncrementPercentage { get; set; }
    public List<Employee> Employees { get; set; }

    
    public Seniority() { }

    
    public Seniority(string level, float baseSalary, float incrementPercentage, List<Employee> employees)
    {
        Level = level;
        BaseSalary = baseSalary;
        IncrementPercentage = incrementPercentage;
        Employees = employees;
    }
}
