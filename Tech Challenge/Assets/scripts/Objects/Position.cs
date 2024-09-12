using System;
using System.Collections.Generic;

namespace Model
{
    public class Position
    {
        public int Id { get; set; }
        public string PositionName { get; set; } 
        public List<string> Seniorities { get; set; } 
        public List<string> BaseSalaries { get; set; }
        public List<string> Increases { get; set; }
        public List<string> Counts { get; set; }

        // Constructor sin parámetros

        public Position()
        {
        }

        // Constructor con parámetros
        public Position(int id, string positionName, List<string> seniorities, List<string> baseSalaries, List<string> increases, List<string> counts)
        {
            Id = id;
            PositionName = positionName;       
            Seniorities = seniorities;           
            BaseSalaries = baseSalaries;        
            Increases = increases;               
            Counts = counts;                     
        }
    }
}
