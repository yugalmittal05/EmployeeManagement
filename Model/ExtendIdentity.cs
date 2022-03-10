using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Model
{
  public class ExtendIdentity : IdentityUser
  {
    public string Name { get; set; }
    public string Gender { get; set; }
    public DateTime BirthDate { get; set; }
  }
}
