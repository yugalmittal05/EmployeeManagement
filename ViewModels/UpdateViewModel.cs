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
  public class UpdateViewModel : EmployeeCreateViewModel
  {
    public int Id { get; set; }
    
    public string OldPhoto { get; set; }
   
    [Remote(action:"IsEmail",controller:"home")]
    [EmailAddress]
    public string NewEmail { get; set; }
  }
}
