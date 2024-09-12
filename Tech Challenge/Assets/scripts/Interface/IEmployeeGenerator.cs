using System;
using System.Collections.Generic;
using Model;

namespace Interface
    {
        public interface IEmployeeGenerator
        {
            List<Employee> GenerateEmployees(List<string[]> positionData);
        }

    }