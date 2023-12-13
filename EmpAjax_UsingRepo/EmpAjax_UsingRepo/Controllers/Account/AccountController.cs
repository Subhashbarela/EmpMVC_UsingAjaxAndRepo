using EmpAjax_UsingRepo.DataContext;
using EmpAjax_UsingRepo.Models.Account;
using EmpAjax_UsingRepo.Models.ViewModel;
using EmpAjax_UsingRepo.Repository.Interface;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System;
using System.Threading.Tasks;

namespace EmpAjax_UsingRepo.Controllers.Account
{
    public class AccountController : Controller
    {
        private readonly IUserRepo _userRepo;

        public AccountController(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> Login(UserLogin model)
        //{
        //    User user = new User();
        //    if (model != null)
        //    {
        //         user =await _userRepo.UserLogin(model);

        //        if (user != null)
        //        {
        //            HttpContext.Session.SetInt32("UserId", user.Id);
        //            HttpContext.Session.SetString("UserName",user.UserName);
        //            return RedirectToAction("Index", "Home");
        //        }
        //        else
        //        {
        //            TempData["errorMessage"] = "userName and id not match please signUp";
        //            return View("Login", model);
        //        }
        //    }
        //    else
        //    {
        //        TempData["errorMessage"] = "User Model is null";
        //        return View("Login", model);
        //    }
        //}
        [HttpPost]
        public async Task<IActionResult> Login(LoginSignUpViewModel model, bool rememberMe)
        {
            if (ModelState.IsValid)
            {
                var user = await _userRepo.UserLogin(model);

                if (user != null)
                {
                    // Create a claims identity for the user                  
                    bool valid = (user.UserName == model.UserName && user.Password == model.Password);
                    if (valid)
                    {
                        var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, model.UserName) },
                          CookieAuthenticationDefaults.AuthenticationScheme);
                        var principle= new ClaimsPrincipal(identity);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principle);
                        HttpContext.Session.SetString("Username",model.UserName);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["errorPassword"] = " Invalid password";
                        return View(model);
                    }
                }
                else
                {
                    TempData["errorUsername"] = "Invalid Username ";
                    return View( model);
                }
            }
            else
            {
                return View( model);
            }
        }
        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var storeCookies = Request.Cookies.Keys;
            foreach ( var cookie in storeCookies)
            {
                Response.Cookies.Delete(cookie);
            }
            return RedirectToAction("Login", "Account");
        }

        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(SignUpUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool status = _userRepo.SignUpUser(model);
                if (status)
                {
                    TempData["successMessage"] = "You are elligible to login,please fill own credential's then login";
                    return RedirectToAction("Login");
                }
                else
                {
                    TempData["errorMessage"] = "Empty form can't be submitted!";

                    return View(model);
                }
            }
            return View(model);
        }
    }
}
