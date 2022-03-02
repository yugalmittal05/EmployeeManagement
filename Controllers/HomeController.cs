using EmployeeManagement.Model;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers
{
  [Route("/[controller]/")]
  public class HomeController : Controller
  {
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IWebHostEnvironment webHostEnvironment;
    

    public HomeController(IEmployeeRepository employeeRepository,
      IWebHostEnvironment webHostEnvironment)
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
        return View();
      
    }
    [HttpGet("Create")]
    public ViewResult Create()
    {
      return View();
    }

    [HttpPost("Create")]
    public IActionResult Create(EmployeeCreateViewModel model)
    {
      if (ModelState.IsValid)
      {
        string uniqueFileName = null;
        if(model.Photo != null)
        {
         // var uploadFolder = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location.Substring(0, Assembly.GetEntryAssembly().Location.IndexOf("Images")));
          string uploadsFolder = Path.Combine("./wwwroot/","Images");
          uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
          string filePath = Path.Combine(uploadsFolder, uniqueFileName);
          model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
        }
        Employee newEmployee = new Employee
        {
          Name = model.Name,
          Email = model.Email,
          Department = model.Department,
          PhotoPath = uniqueFileName
        };
          _employeeRepository.AddEmployee(newEmployee);
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



    [HttpPost("update/{id?}")]
    public IActionResult Update(Employee employee)
    {
      if (ModelState.IsValid)
      {
        Employee updateEmployee = new Employee
        {
          Name = employee.Name,
          Email = employee.Email,
          Department = employee.Department,
          PhotoPath = employee.PhotoPath
        };
         _employeeRepository.UpdateEmployee(employee);
      return RedirectToAction("details", new { id = updateEmployee.Id });
    }
       return View(employee);
    }

  }
}
