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
            new Employee() { Id = 1, Image ="Images/1.jpg", Name = "Mary", Department = Dept.HR, Email = "mary@pragimtech.com" },
            new Employee() { Id = 2, Image ="Images/2.jpg",Name = "John", Department = Dept.IT, Email = "john@pragimtech.com" },
            new Employee() { Id = 3, Image ="Images/3.jpg",Name = "Sam", Department = Dept.IT, Email = "sam@pragimtech.com" },
        };
    }

    public  Employee  GetEmployee(int id)
    {
      return this._employeeList.FirstOrDefault(e => e.Id == id);
    }

    public IEnumerable<Employee> GetAllEmployees()
    {
      return  _employeeList;
    }

    //delete Employee
    public bool DeleteEmployee(int id)
    {
      var delUser = _employeeList.Where(e => e.Id == id).FirstOrDefault();
      bool status = _employeeList.Remove(delUser);
     if(status == true)
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    //add employee
    public Employee AddEmployee(Employee employee)
    {
      employee.Id =  _employeeList.Max(e => e.Id + 1);
      _employeeList.Add(employee);
      return employee;
    }

    public Employee UpdateEmployee(Employee employee)
    { 
      var updateUser = _employeeList.Where(e => e.Id == employee.Id).FirstOrDefault();
      if (updateUser != null)
      {
        updateUser.Name = employee.Name;
        updateUser.Email = employee.Email;
        updateUser.Department = employee.Department;
      }
      return employee;
    }
  }
}
