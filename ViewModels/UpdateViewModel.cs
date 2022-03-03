using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.ViewModels
{
  public class UpdateViewModel : EmployeeCreateViewModel
  {
    public int Id { get; set; }
    public string OldPhoto { get; set; }
  }
}
