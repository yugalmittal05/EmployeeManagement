using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers
{
  public class ErrorController : Controller
  {
    [Route("Error")]
    [Route("Error/{statusCode}")]
    public ActionResult HttpStatusCodeHandler(int statusCode)
    {
     /* var statusCodeResult =
              HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

      switch (statusCode)
      {
        case 404:
          ViewBag.ErrorMessage =
                  "Sorry, the resource you requested could not be found";
          //ViewBag.Path = statusCodeResult.OriginalPath;
          //ViewBag.QS = statusCodeResult.OriginalQueryString;
          break;
      }*/
      return View("404Error");
    }
  }
  
}
