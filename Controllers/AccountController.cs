using EmployeeManagement.Model;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers
{
  [AllowAnonymous]
  public class AccountController : Controller
  {
    private readonly UserManager<ExtendIdentity> userManager;
    private readonly SignInManager<ExtendIdentity> signInManager;
    private readonly RoleManager<IdentityRole> roleManager;

    public AccountController(UserManager<ExtendIdentity> userManager,
                               SignInManager<ExtendIdentity> signInManager,
                               RoleManager<IdentityRole> roleManager)
    {
      this.userManager = userManager;
      this.signInManager = signInManager;
      this.roleManager = roleManager;
    }

    // To Get Login View
    [HttpGet]
    public IActionResult LogIn()
    {
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model,string returnUrl)
    {
      if (ModelState.IsValid)
      {
      var result = await signInManager.PasswordSignInAsync(model.Email, model.Password,
                          model.RememberMe,false);
      if (result.Succeeded)
      {
          if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
          {
              return Redirect(returnUrl);
          }
          else
          {
            return RedirectToAction("index", "home");
          }
       
      }
        ModelState.AddModelError(string.Empty, "Invalid User Email or Password.");
      }
      return View(model);
    }
    
    // Logout user
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
      await signInManager.SignOutAsync();
      return RedirectToAction("index", "home");
    }

    [AcceptVerbs("get","post")]
    [AllowAnonymous]
    public async Task<IActionResult> IsEmail(string email)
    {
      var Email = await userManager.FindByEmailAsync(email);
      if(Email == null)
      {
        return Json(true);
      }
      else
      {
        return Json("This Email is Already Used By another person.");
      }

    }
    
    public async Task<IActionResult> Profile(string userName)
    {
      var user = await userManager.FindByNameAsync(userName);
      ProfileViewModel profileViewModel = new ProfileViewModel
      {
        Name = user.Name,
        //Role = roleManager.GetRoleNameAsync( ),
        Email = user.Email,
        BirthDate = user.BirthDate,
        Gender = user.Gender,

        //Password = PasswordHasher<User>
      };
      return View(profileViewModel);
    }

    // User Register
    [HttpGet]
    public IActionResult Register()
    {
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
      if (ModelState.IsValid)
      {
        var user = new ExtendIdentity
        {
          Name = model.Name,
          Gender = model.Gender,
          BirthDate = model.BirthDate,
          UserName = model.Email,
          Email = model.Email,

        };
        var result = await userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
          await signInManager.SignInAsync(user, isPersistent: false);
          return RedirectToAction("index", "home");
        }
        foreach(var error in result.Errors)
        {
          ModelState.AddModelError(string.Empty, error.Description);
        }
      }
      return View(model);
    }
  }
}
