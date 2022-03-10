﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.ViewModels
{
  public class RegisterViewModel
  {

    [Required]
    public string Name { get; set; }
    
    [Required]
    [Remote(action:"IsEmail",controller:"account")]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Gender { get; set; }

    [Required]
    public DateTime BirthDate { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password",
        ErrorMessage = "Password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
  }
}
