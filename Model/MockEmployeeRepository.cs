using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace EmployeeManagement.Model
{
  public class MockEmployeeRepository : IEmployeeRepository
  {
    private List<Employee> _employeeList;

    public MockEmployeeRepository()
    {
      _employeeList = new List<Employee>()
        {
            new Employee() { Id = 1,  Name = "Mary", Department = Dept.HR, Email = "mary@pragimtech.com" },
            new Employee() { Id = 2, Name = "John", Department = Dept.IT, Email = "john@pragimtech.com" },
            new Employee() { Id = 3, Name = "Sam", Department = Dept.IT, Email = "sam@pragimtech.com" },
        };
    }

    //Get Data by id
    public  Employee  GetEmployee(int id)
    {
      return this._employeeList.FirstOrDefault(e => e.Id == id);
    }

    //Get All Employees Data
    public IEnumerable<Employee> GetAllEmployees()
    {
      return  _employeeList;
    }

    //delete Employee
    public Employee DeleteEmployee(int id)
    {
      Employee employee = _employeeList.Where(e => e.Id == id).FirstOrDefault();
      if (employee != null)
      {
        _employeeList.Remove(employee);
      }
      return employee;
    }

    //add employee
    public Employee AddEmployee(Employee employee)
    {
      employee.Id =  _employeeList.Max(e => e.Id + 1);
      _employeeList.Add(employee);
      return employee;
    }

    //update Employee
    public Employee UpdateEmployee(Employee employee)
    { 
      var updateUser = _employeeList.Where(e => e.Id == employee.Id).FirstOrDefault();
      if (updateUser != null)
      {
        updateUser.Name = employee.Name;
        updateUser.Email = employee.Email;
        updateUser.Department = employee.Department;
       // Console.Write(updateUser.Email);
      }
      return updateUser;
    }
  }
}
