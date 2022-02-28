using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Model
{
  public class Employee
  {
    public int Id { get; set; }
    public string Image { get; set; }
    [Required(ErrorMessage = "Invaild Name")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Invaild Mail")]
    [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",ErrorMessage = "Invalid email format")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Please Select Your Department")]
    public Dept Department { get; set; }
  }
}
