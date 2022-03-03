using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Model
{
  public class SQLEmployeeRepository : IEmployeeRepository
  {
    private readonly AppDbContext context;

    public SQLEmployeeRepository(AppDbContext context)
    {
      this.context = context;
    }

    public Employee AddEmployee(Employee employee)
    {
      context.Employees.Add(employee);
      context.SaveChanges();
      return employee;
    }

    public Employee DeleteEmployee(int Id)
    {
      Employee employee = context.Employees.Where(e => e.Id == Id).FirstOrDefault();
      if (employee != null)
      {
        context.Employees.Remove(employee);
        context.SaveChanges();
      }
      return employee;
    }

    public IEnumerable<Employee> GetAllEmployees()
    {
      return context.Employees;
    }

    public Employee GetEmployee(int Id)
    {
      var user = context.Employees.Find(Id);
      return user;
    }

    public Employee UpdateEmployee(Employee employeeChanges)
    {
      var employee = context.Employees.Attach(employeeChanges);
      employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
      context.SaveChanges();
      return employeeChanges;
    }
  }
}
