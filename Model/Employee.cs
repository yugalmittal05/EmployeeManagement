using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Model
{
  public class Employee
  {
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Invaild Name")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Invaild Mail")]
    //[Remote(action:"IsEmail",controller:"Home")]
    [EmailAddress]
    public string Email { get; set; }
    [Required(ErrorMessage = "Please Select Your Department")]
    public Dept? Department { get; set; }
    public string PhotoPath { get; set; }
  }
}
