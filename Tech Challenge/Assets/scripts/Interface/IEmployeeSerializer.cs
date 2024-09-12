using System.Collections.Generic;
using Model;

namespace Interface
{
    public interface IEmployeeSerializer
    {
        void SaveEmployeesToXml(List<Employee> employees, string fileName);
    }

}