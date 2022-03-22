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

        #region Login, Logout

        // To Get Login View
        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password,
                                    model.RememberMe, false);
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
        #endregion

        #region remote Validation 

        [AcceptVerbs("get", "post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsEmail(string email)
        {
            var Email = await userManager.FindByEmailAsync(email);
            if (Email == null)
            {
                return Json(true);
            }
            else
            {
                return Json("This Email is Already Used By another person.");
            }

        }

        #endregion

        #region "Profile and listOf Users"

        /// <summary>
        /// View the profile Of active user 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        
        [HttpPost]
        [HttpGet]
        public async Task<ViewResult> Profile(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);
            var role = await userManager.GetRolesAsync(user);
            ProfileViewModel profileViewModel = new ProfileViewModel
            {
                Name = user.Name,
                Role = role.LastOrDefault(),  
                Email = user.Email,
                BirthDate = user.BirthDate,
                Gender = user.Gender,

                //Password = PasswordHasher<User>
            };
            return View(profileViewModel);
        }

        /// <summary>
        /// View the list Of all register Users in admin Panel
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ListUsers()
        {
            var user = userManager.Users;

            return View(user);
        }
        #endregion

        #region "User Related Functions : DeleteUser, Register User"
        /// <summary>
        /// WILL DELETE USER ON BASIS OF USER ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return View("404error");
            }
            else
            {
                var result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListUsers");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(user);
            }
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
                    if (!signInManager.IsSignedIn(User))
                    {
                        await signInManager.SignInAsync(user, isPersistent: false);
                        await userManager.AddToRoleAsync(user, "User");
                        return RedirectToAction("index", "home");
                    }
                    else
                    {
                        return RedirectToAction("ListUsers", "account");
                        
                    }
                   
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }
        #endregion
    }
}
