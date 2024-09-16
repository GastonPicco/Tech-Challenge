using System;
using System.Collections.Generic;

public class Position
{
    public string JobTitle { get; set; }
    public List<Seniority> Seniorities { get; set; }

   
    public Position() { }


    public Position(string jobTitle, List<Seniority> seniorities)
    {
        JobTitle = jobTitle;
        Seniorities = seniorities;
    }
}
