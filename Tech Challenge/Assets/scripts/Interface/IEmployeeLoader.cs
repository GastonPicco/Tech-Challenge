using System.Collections.Generic;
using Model;

namespace Interface
{
    public interface IEmployeeLoader
    {
        public List<Employee> LoadEmployeesFromXml(string fileName);
    }

}