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
    private readonly UserManager<IdentityUser> userManager;
    private readonly SignInManager<IdentityUser> signInManager;

    public AccountController(UserManager<IdentityUser> userManager,
                               SignInManager<IdentityUser> signInManager)
    {
      this.userManager = userManager;
      this.signInManager = signInManager;
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
        ModelState.AddModelError(string.Empty, "Invalid User Login Attempt");
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
        var user = new IdentityUser
        {
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
