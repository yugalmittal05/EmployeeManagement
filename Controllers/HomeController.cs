using EmployeeManagement.Model;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers
{
  [Route("/[controller]/")]
  public class HomeController : Controller
  {
    private readonly IEmployeeRepository _employeeRepository;

    public HomeController(IEmployeeRepository employeeRepository)
    {
      _employeeRepository = employeeRepository;
    }

   
    [HttpGet("/")]
    [HttpGet("index")]
    //Get all Employees data
    public ViewResult Index()
    {
      var model = _employeeRepository.GetAllEmployees();
      ViewBag.PageTitle = "Employee List";
      return View(model);
    }

    [HttpGet("details/{id?}")]
    // Get employee Data By id
    public ViewResult Details(int id)
    {
      HomeDetailsViewModel homeDetailsViewModels = new HomeDetailsViewModel()
      {
        Employee = _employeeRepository.GetEmployee(id),
        PageTitle = "Employee Details"
      };
      
      if (homeDetailsViewModels != null)
      {
        return View(homeDetailsViewModels);
      }
      else
      {
        return View();
      }
    }

    [Route("delete/{id?}")]
    public ActionResult Delete(int id)
    {
     var checkStatus = _employeeRepository.DeleteEmployee(id);
      ViewBag.PageTitle = "Employee Delete";
      if (checkStatus)
      {
        ViewBag.checkStatus = checkStatus;
        return View();
      }
      else
      {
        return View();
      }
    }
    [HttpGet("Create")]
    public ViewResult Create()
    {
      return View();
    }

    [HttpPost("Create")]
    public IActionResult Create(Employee employee)
    {
      if (ModelState.IsValid)
      {
        Employee newEmployee = _employeeRepository.AddEmployee(employee);
        return RedirectToAction("details", new { id = newEmployee.Id });
      }
      return View();
    }
    [HttpGet("update/{id?}")]
    public ViewResult Update(int id)
    {
      var e = _employeeRepository.GetEmployee(id);
      return View(e);
    }

    [Route("update/{id?}")]
    public IActionResult Update(Employee employee)
    {
      if (ModelState.IsValid)
      {
        Employee updateEmployee = _employeeRepository.UpdateEmployee(employee);
      return RedirectToAction("details", new { id = updateEmployee.Id });
    }
       return View(employee);
    }

  }
}
