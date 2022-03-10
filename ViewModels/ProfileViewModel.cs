using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.ViewModels
{
  public class ProfileViewModel : RegisterViewModel
  {
    public string UserName { get; set; }
    public string Role { get; set; }
  }
}
