using EmployeeManagement.Model;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeManagement.Controllers
{
  [Authorize]
  [Route("[controller]")]
  public class HomeController : Controller
  {
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IWebHostEnvironment Environment;


    public HomeController(IEmployeeRepository employeeRepository,
      IWebHostEnvironment webHostEnvironment)
    {
      _employeeRepository = employeeRepository;
      this.Environment = webHostEnvironment;
    }

    [AllowAnonymous]
    [HttpGet("/")]
    [HttpGet("index")]
    //Get all Employees data
    public ViewResult Index()
    {
      var model = _employeeRepository.GetAllEmployees();
      ViewBag.PageTitle = "Employee List";
      return View(model);
    }

    // Get employee Data By id

    [HttpGet("Details/{id?}")]
    public ViewResult Details(int id)
    {
     // throw new Exception("New Error By Details");
      Employee employee = _employeeRepository.GetEmployee(id);
      if (employee == null)
      {
        //Response.StatusCode = 404;
        return View("404Error");
      }
      else
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
    }

    //delete Data by Id
    [Route("delete/{id?}")]
    public IActionResult Delete(int id)
    {
      Employee employee = _employeeRepository.DeleteEmployee(id);
      //_employeeRepository.DeleteEmployee(id);
      ViewBag.PageTitle = "Employee Delete";
      
      if (string.IsNullOrEmpty(employee.PhotoPath) !=true)
      {
        string photoPath = Environment.WebRootPath + "/images/" + employee.PhotoPath;
        System.IO.File.Delete(photoPath);
      }
      return View();

    }

    [AcceptVerbs("get", "post")]
    [AllowAnonymous]
    public IActionResult IsEmail(string email)
    {
      //var user = _employeeRepository.GetEmployeeByEmail(email);
      if (email != _employeeRepository.GetEmployeeByEmail(email))
      {
        return Json(true);
      }
      else
      {
        return Json($"This Email, {email} is Already Used By another person.");
      }
    }

    
    // Create view
    [HttpGet("Create")]
    public ViewResult Create()
    {
      return View();
    }

    //create data
    [HttpPost("Create")]
    public IActionResult Create(EmployeeCreateViewModel model)
    {
        if (ModelState.IsValid)
        {
          string uniqueFileName = ProcesFileUpload(model);
          Employee newEmployee = new Employee
          {
            Name = model.Name,
            Email = model.Email.ToLower(),
            Department = model.Department,
            PhotoPath = uniqueFileName
          };
          _employeeRepository.AddEmployee(newEmployee);
          return RedirectToAction("details", new { id = newEmployee.Id });
      }
      return View();
    }

    

    //Get update View 
    [HttpGet("update/{id?}")]
    public ViewResult Update(int id)
    {
      Employee employee = _employeeRepository.GetEmployee(id);
      UpdateViewModel updateViewModel = new UpdateViewModel
      {
        Id = employee.Id,
        Name = employee.Name,
        Email = employee.Email,
        NewEmail = employee.Email,
        Department = employee.Department,
        OldPhoto = employee.PhotoPath
      };
      return View(updateViewModel);
    }


    //Upadte Data By Id
    [HttpPost("update/{id?}")]
    public IActionResult Update(UpdateViewModel model)
    {
      if (ModelState.IsValid)
      {
        Employee employee = _employeeRepository.GetEmployee(model.Id);
        employee.Name = model.Name;
       // employee.Email = model.NewEmail;
        employee.Department = model.Department;
        if (model.Photo != null)
        {
          if (model.OldPhoto != null)
          {
            string filePath = Path.Combine("./wwwroot/", "images", model.OldPhoto);
            System.IO.File.Delete(filePath);
          }
          employee.PhotoPath = ProcesFileUpload(model);
        }
        _employeeRepository.UpdateEmployee(employee);
        return RedirectToAction("details");
      }
      return View();
    }
    //Get File Name And Upload in Folder
    private string ProcesFileUpload(EmployeeCreateViewModel model)
    {
      string uniqueFileName = null;
      if (model.Photo != null)
      {
        // var uploadFolder = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location.Substring(0, Assembly.GetEntryAssembly().Location.IndexOf("Images")));
        string uploadsFolder = Path.Combine(Environment.WebRootPath, "Images");
        //string uploadsFolder = Path.Combine("./wwwroot/","Images");
        uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
        using (var filestream = new FileStream(filePath, FileMode.Create))
        {
          model.Photo.CopyTo(filestream);
        }
      }
      return uniqueFileName;
    }
  }
}
