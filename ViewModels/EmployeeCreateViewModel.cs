using EmployeeManagement.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.ViewModels
{
  public class EmployeeCreateViewModel
  {
   
    [Required(ErrorMessage = "Invaild Name")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Invaild Mail")]
    [Remote(action: "IsEmail", controller: "Home")]
    [EmailAddress] 
    public string Email { get; set; }

    [Required(ErrorMessage = "Please Select Your Department")]
    public Dept? Department { get; set; }
    
    public IFormFile Photo { get; set; }
  }
}
