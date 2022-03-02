using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Model
{
  public interface IEmployeeRepository
  {
   Employee  GetEmployee(int id);
   IEnumerable<Employee> GetAllEmployees();

   Employee DeleteEmployee(int id);

   Employee AddEmployee(Employee employee);

   Employee UpdateEmployee(Employee employee);
  }
}
